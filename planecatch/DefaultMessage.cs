using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace planecatch
{
    public class DefaultMessage : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;

        public DefaultMessage(Game game)
            : base(game)
        {
        }

        

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _font = Game.Content.Load<SpriteFont>("ClockFont");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Encontre a entrada para o galpao do aviao!", new Vector2(10, 10), Color.Blue);
            _spriteBatch.DrawString(_font, (Game as PlaneCatchGame).Camera.Position.ToString(), new Vector2(10, 30), Color.Blue);
            _spriteBatch.End();

            base.Draw(gameTime);


            base.Draw(gameTime);
        }
    }
}