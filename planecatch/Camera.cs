using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace planecatch
{
    public class Camera : GameComponent
    {

        public Matrix View { get; set; }
        public Matrix Projection { get; set; }
        public Vector3 Position { get; set; }
        public IPlayerState CurrentState { get; set; }

        private Vector3 TargetDirection { get; set; }
        private Vector3 Up { get; set; }
        private Vector3 SideVector
        {
            //Para andar de lado, basta encontrar o produto vetorial entre o vetor Up e o vetor que aponta a direção do target.
            get { return Vector3.Cross(Up, WalkingDirection); }
        }

        private Vector3 WalkingDirection
        {
            //Para o jogador não ficar voando, é necessário remover todo o valor y da direção pra onde ele vai andar.
            //Para que a velocidade dele não caia, é preciso adicionar no x o que foi tirado do y;
            get { return TargetDirection + new Vector3(TargetDirection.Y, -TargetDirection.Y, 0); }
        }

        public Vector3 Velocity { get; set; }

        public bool Jumping { get; set; }

        private MouseState _previousMouseState;
        private IPlayerState _walkingState;
        private IPlayerState _runningState;
        
        
        public Camera(Game game, Vector3 position, Vector3 target, Vector3 up) : base(game)
        {
            Position = position;
            TargetDirection = target - position;
            TargetDirection = Vector3.Normalize(TargetDirection);
            Up = up;

            InitializePhysics();

            InitializeStates();

            CreateLookAt();

            InitializeProjection(game);
        }

        private void InitializePhysics()
        {
            Velocity = Vector3.Zero;
        }

        private void InitializeProjection(Game game)
        {
            var aspectRatio = game.Window.ClientBounds.Width / (float)game.Window.ClientBounds.Height;

            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                                             aspectRatio, 1, 3000);
        }

        private void InitializeStates()
        {
            _walkingState = new Walking();
            _runningState = new Running();
        }

        private void CreateLookAt()
        {
            var target = Position + TargetDirection;
            View = Matrix.CreateLookAt(Position, target, Up);
        }

        private void HandleInput()
        {
            var helper = ((PlaneCatchGame) Game).InputHelper;

            helper.ExecuteIf(
                (s) => s.IsKeyDown(Keys.LeftShift) || s.IsKeyDown(Keys.RightShift),
                ToRunningState
                );
            
            //Se os dois shifts estiverem soltos, o jogador está andando.
            helper.ExecuteIf(
                (s) => s.IsKeyUp(Keys.LeftShift) && s.IsKeyUp(Keys.RightShift),
                ToWalkingState
                );

            helper.ExecuteIfKeyPressed(Keys.W, Forward);
            helper.ExecuteIfKeyPressed(Keys.S, Backwards);
            helper.ExecuteIfKeyPressed(Keys.A, StrafeLeft);
            helper.ExecuteIfKeyPressed(Keys.D, StrafeRight);
            helper.ExecuteIfJustPressed(Keys.Space, Jump);

            helper.ExecuteIf(
                s => 
                    s.IsKeyUp(Keys.W) && 
                    s.IsKeyUp(Keys.A) && 
                    s.IsKeyUp(Keys.S) && 
                    s.IsKeyUp(Keys.D)
                , Stand
                );
        }

        private void Jump()
        {
            Velocity += new Vector3(0, 10, 0);
            Jumping = true;
        }

        private void StrafeRight()
        {
            Velocity += -(SideVector * CurrentState.Speed);
        }

        private void StrafeLeft()
        {
            Velocity += (SideVector * CurrentState.Speed);
        }

        private void Backwards()
        {
            Velocity += -(WalkingDirection * CurrentState.Speed);
        }

        private void Forward()
        {
            Velocity += WalkingDirection * CurrentState.Speed;
        }

        private void Stand()
        {
            ResetVelocity();
        }

        private void ResetVelocity()
        {
            Velocity -= new Vector3(Velocity.X, 0, Velocity.Z);
        }

        private void UpdatePosition()
        {
            Position += Velocity;
            ResetVelocity();
        }

        private void ToRunningState()
        {
            CurrentState = _runningState;
        }

        private void ToWalkingState()
        {
            CurrentState = _walkingState;
        }

        private void Pitch()
        {
            var angle = (MathHelper.PiOver4 / 150) * (Mouse.GetState().Y - _previousMouseState.Y);

            TargetDirection = Vector3.Transform(TargetDirection,
                                          Matrix.CreateFromAxisAngle(Vector3.Cross(Up, TargetDirection) * CurrentState.Speed,
                                                                    angle));
        }

        private void Yaw()
        {
            var angle = (-MathHelper.PiOver4 / 150) * (Mouse.GetState().X - _previousMouseState.X);

            TargetDirection = Vector3.Transform(TargetDirection,
                                          Matrix.CreateFromAxisAngle(Up,
                                                                    angle));
        }

        private void ResetMouse()
        {
            var middleWidth = Game.Window.ClientBounds.Width / 2;
            var middleHeight = Game.Window.ClientBounds.Height / 2;

            Mouse.SetPosition(middleWidth, middleHeight);
        }

        public override void Initialize()
        {
            base.Initialize();

            ResetMouse();

            _previousMouseState = Mouse.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();

            UpdatePosition();

            Yaw();
            Pitch();

            CreateLookAt();

            ResetMouse();

            base.Update(gameTime);
        }
    }
}
