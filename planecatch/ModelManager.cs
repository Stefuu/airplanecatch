using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace planecatch
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ModelManager : DrawableGameComponent, IModelManager
    {
        public List<BasicModel> Models { get; private set; }

        public ModelManager(Game game)
            : base(game)
        {
            Game.Services.AddService(typeof(IModelManager), this);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            Models = new List<BasicModel>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Models.Add(new Skybox(Game.Content.Load<Model>("Models\\skybox")));
            Models.Add(new BasicModel(Game.Content.Load<Model>("Models\\airport")));

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (var basicModel in Models)
            {
                basicModel.Update();   
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var basicModel in Models)
            {
                basicModel.Draw(((PlaneCatchGame)Game).Camera, Game.GraphicsDevice);
            }

            base.Draw(gameTime);
        }
    }
}