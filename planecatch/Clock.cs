using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace planecatch
{
    public class Clock : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        public Clock(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            _font = Game.Content.Load<SpriteFont>("ClockFont");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            var current = GetRemainingTime(gameTime);

            if (current <= TimeSpan.Zero)
                //Adicionar código para terminar o jogo.

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            var current = GetRemainingTime(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, current.ToString(), new Vector2(570,20), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private TimeSpan GetRemainingTime(GameTime gameTime)
        {
            var elapsedTime = gameTime.TotalRealTime;
            var maximum = new TimeSpan(0, 1, 0);

            return maximum - elapsedTime;
        }
    }
}