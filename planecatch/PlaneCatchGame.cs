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
    /// This is the main type for your game
    /// </summary>
    public class PlaneCatchGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;

        public Camera Camera { get; set; }
        public ModelManager ModelManager { get; set; }
        public InputHelper InputHelper { get; set; }
        public Gravity Gravity { get; set; }
        public Collision Collision { get; set; }
        public Clock Clock { get; set; }
        public DrawableGameComponent CurrentMessage { get; set; }
        public WinningConditionChecker WinningConditionChecker { get; set; }

        public PlaneCatchGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Camera = new Camera(this, new Vector3(0,1.6f,50), Vector3.Zero, Vector3.Up);
            ModelManager = new ModelManager(this);
            InputHelper = new InputHelper(this);
            Gravity = new Gravity(this);
            Collision = new Collision(this);
            Clock = new Clock(this);
            CurrentMessage = new DefaultMessage(this);
            WinningConditionChecker = new WinningConditionChecker(this);


            Camera.UpdateOrder = 0;
            ModelManager.UpdateOrder = 1;
            InputHelper.UpdateOrder = 2;
            Gravity.UpdateOrder = 3;
            Collision.UpdateOrder = 4;
            Clock.UpdateOrder = 5;
            CurrentMessage.UpdateOrder = 6;
            WinningConditionChecker.UpdateOrder = 7;

            Components.Add(Camera);
            Components.Add(ModelManager);
            Components.Add(InputHelper);
            Components.Add(Gravity);
            Components.Add(Collision);
            Components.Add(Clock);
            Components.Add(CurrentMessage);
            Components.Add(WinningConditionChecker);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //_spriteBatch = new SpriteBatch(GraphicsDevice);
            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            InputHelper.ExecuteIfKeyPressed(Keys.Escape, Exit);

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
