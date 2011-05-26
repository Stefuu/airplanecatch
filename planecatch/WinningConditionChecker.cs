using System;
using Microsoft.Xna.Framework;

namespace planecatch
{
    public class WinningConditionChecker : GameComponent
    {
        public WinningConditionChecker(Game game) : base(game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            var player = (Game as PlaneCatchGame).Camera;

            if (player.Position.X > 100 && player.Position.X < 105)
                if (-player.Position.Z > 28 && -player.Position.Z < 33)
                    Win();


            base.Update(gameTime);
        }

        private void Win()
        {
            var gameWonMessage = new GameWonMessage(Game);
            ((PlaneCatchGame)Game).CurrentMessage = gameWonMessage;
            Game.Components.Add(gameWonMessage);
        }
    }
}