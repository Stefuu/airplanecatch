using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace planecatch
{
    public class GameLostMessage : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        public GameLostMessage(Game game) : base(game)
        {
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            _font = Game.Content.Load<SpriteFont>("ClockFont");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Perdeu!!" + Environment.NewLine + "Tente encontrar o aviao antes que o tempo acabe...", new Vector2(200, 200), Color.DarkRed);
            _spriteBatch.End();

            base.Draw(gameTime);


            base.Draw(gameTime);
        }
    }
}