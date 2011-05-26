using Microsoft.Xna.Framework;

namespace planecatch
{
    public class Collision : GameComponent
    {
        private PlaneCatchGame PlaneCatchGame { get { return (PlaneCatchGame)Game; } }


        public Collision(Game game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            var player = PlaneCatchGame.Camera;


            //TODO: Alterar de 0 para o y do objeto diretamente abaixo.
            if (player.Position.Y > 1.6f) return;

            player.Velocity = new Vector3(player.Velocity.X, 0, player.Velocity.Z);
            player.Position = new Vector3(player.Position.X, 1.6f, player.Position.Z);
            player.Jumping = false;

            base.Update(gameTime);
        }
    }
}