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

        private Vector3 Direction { get; set; }
        private Vector3 Up { get; set; }
        private MouseState _previousMouseState;
        


        private const float Speed = 3;

        public override void Initialize()
        {
            base.Initialize();

            Mouse.SetPosition(Game.Window.ClientBounds.Width / 2, this.Game.Window.ClientBounds.Height / 2);
            _previousMouseState = Mouse.GetState();
        }
        
        public Camera(Game game, Vector3 position, Vector3 target, Vector3 up) : base(game)
        {
            Position = position;
            Direction = position - target;
            Direction = Vector3.Normalize(Direction);
            Up = up;

            CreateLookAt();

            var aspectRatio = game.Window.ClientBounds.Width / (float)game.Window.ClientBounds.Height;

            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                                                             aspectRatio, 1, 3000);
        }

        public void CreateLookAt()
        {
            var target = Position + Direction;
            View = Matrix.CreateLookAt(Position, target, Up);
        }

        public override void Update(GameTime gameTime)
        {
            InputHelper.ExecuteIfKeyPressed(Keys.W, () => Position += Direction * Speed);
            InputHelper.ExecuteIfKeyPressed(Keys.S, () => Position -= Direction * Speed);
            InputHelper.ExecuteIfKeyPressed(Keys.A, () => Position += Vector3.Cross(Up, Direction) * Speed);
            InputHelper.ExecuteIfKeyPressed(Keys.D, () => Position -= Vector3.Cross(Up, Direction) * Speed);

            Direction = Vector3.Transform(Direction,
                Matrix.CreateFromAxisAngle(Up,
                (-MathHelper.PiOver4 / 150) * (Mouse.GetState().X - _previousMouseState.X)));
            
            CreateLookAt();


            base.Update(gameTime);
        }
    }
}
