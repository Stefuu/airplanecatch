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


        private Vector3 Direction { get; set; }
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
            get { return Direction + new Vector3(Direction.Y, -Direction.Y, 0); }
        }

        private MouseState _previousMouseState;
        private IPlayerState _walkingState;
        private IPlayerState _runningState;
        
        public Camera(Game game, Vector3 position, Vector3 target, Vector3 up) : base(game)
        {
            Position = position;
            Direction = position - target;
            Direction = Vector3.Normalize(Direction);
            Up = up;

            InitializeStates();

            CreateLookAt();

            var aspectRatio = game.Window.ClientBounds.Width / (float)game.Window.ClientBounds.Height;

            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                                             aspectRatio, 1, 3000);
        }

        private void InitializeStates()
        {
            _walkingState = new Walking();
            _runningState = new Running();
        }

        public override void Initialize()
        {
            base.Initialize();

            ResetMouse();

            _previousMouseState = Mouse.GetState();
        }

        private void CreateLookAt()
        {
            var target = Position + Direction;
            View = Matrix.CreateLookAt(Position, target, Up);
        }

        public override void Update(GameTime gameTime)
        {

            //Se algum shift estiver pressionado, o jogador está correndo.
            InputHelper.ExecuteIf(
                (s) => s.IsKeyDown(Keys.LeftShift) || s.IsKeyDown(Keys.RightShift),
                ToRunningState
                );
            
            InputHelper.ExecuteIf(
                            (s) => s.IsKeyUp(Keys.LeftShift) && s.IsKeyUp(Keys.RightShift),
                            ToWalkingState
                            );

            InputHelper.ExecuteIfKeyPressed(Keys.W, () => Position += WalkingDirection * CurrentState.Speed);
            InputHelper.ExecuteIfKeyPressed(Keys.S, () => Position -= WalkingDirection * CurrentState.Speed);
            InputHelper.ExecuteIfKeyPressed(Keys.A, () => Position += SideVector * CurrentState.Speed);
            InputHelper.ExecuteIfKeyPressed(Keys.D, () => Position -= SideVector * CurrentState.Speed);

            Yaw();
            Pitch();
            
            CreateLookAt();

            ResetMouse();

            base.Update(gameTime);
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

            Direction = Vector3.Transform(Direction,
                                          Matrix.CreateFromAxisAngle(Vector3.Cross(Up, Direction) * CurrentState.Speed,
                                                                    angle));
        }

        private void Yaw()
        {
            var angle = (-MathHelper.PiOver4 / 150) * (Mouse.GetState().X - _previousMouseState.X);

            Direction = Vector3.Transform(Direction,
                                          Matrix.CreateFromAxisAngle(Up,
                                                                    angle));
        }

        private void ResetMouse()
        {
            var middleWidth = Game.Window.ClientBounds.Width / 2;
            var middleHeight = Game.Window.ClientBounds.Height / 2;

            Mouse.SetPosition(middleWidth, middleHeight);
        }
    }
}
