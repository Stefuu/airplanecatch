using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace planecatch
{
    public class GameWonMessage : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        
        public GameWonMessage(Game game) : base(game)
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
            _spriteBatch.DrawString(_font, "Parabens! Voce encontrou a entrada do galpao !!" + Environment.NewLine + "Tecle esc para sair do jogo.", new Vector2(200, 200), Color.Green);
            _spriteBatch.End();

            base.Draw(gameTime);


            base.Draw(gameTime);
        }
    }
}