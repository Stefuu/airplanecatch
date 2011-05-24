using Microsoft.Xna.Framework;

namespace planecatch
{
    public class Gravity : GameComponent
    {
        private PlaneCatchGame PlaneCatchGame { get { return (PlaneCatchGame)Game; } }
        private readonly Vector3 _gravityAccel = new Vector3(0, 0.7f, 0);
        
        public Gravity(Game game) : base(game)
        {
        }
        
        public override void Update(GameTime gameTime)
        {
            var player = PlaneCatchGame.Camera;

            if (player.Jumping)
            {
                player.Velocity -= _gravityAccel;
            }

            base.Update(gameTime);
        }
    }
}