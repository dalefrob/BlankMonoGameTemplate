using BlankMonoGameTemplate.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using System;
using MonoGame.Extended.Screens;
using System.Collections.Generic;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Input.InputListeners;
using BlankMonoGameTemplate.Screens;
using BlankMonoGameTemplate.Engine.Data;
using Microsoft.Xna.Framework.Content;
using BlankMonoGameTemplate.Engine.Data.Map;

namespace BlankMonoGameTemplate
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        //public ScreenGameComponent ScreenComponent { get; private set; }
        public InputListenerComponent InputListenerComponent { get; private set; }
        public MouseListener MouseListener { get; private set; }
        public KeyboardListener KeyboardListener { get; private set; }

        // Screens
        ScreenManagerComponent screenManagerComponent;
        WorldScreen worldScreen;

        public static SpriteFont Mainfont;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = @"Content";

            IsMouseVisible = true;
            IsFixedTimeStep = false;

            // Create components
            screenManagerComponent = new ScreenManagerComponent(this);
            Components.Add(screenManagerComponent);

            KeyboardListener = new KeyboardListener();
            MouseListener = new MouseListener();
            InputListenerComponent = new InputListenerComponent(this, KeyboardListener, MouseListener);
            Components.Add(InputListenerComponent);
    
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
            GameServices.AddService<GraphicsDevice>(GraphicsDevice);
            GameServices.AddService<ContentManager>(Content);
            //CreateDefaultTileset();
            //CreateTestMap();
            screenManagerComponent.AddScreen<WorldScreen>(true);
            screenManagerComponent.AddScreen<MapEditorScreen>();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            Mainfont = Content.Load<SpriteFont>("Fonts/mainfont");
   
        }
      
        void CreateDefaultTileset()
        {
            Console.WriteLine("Creating Test Data");
            var tileset = new Tileset("Default", 16, new string[]{ "Floor", "Wall", "Decor0" });
            Helper.SaveTileset(tileset);         
        }

        void CreateTestMap()
        {
            Map map = Map.CreateNew("NewTestmap", 32, 32, 16, 2);
            Helper.SaveMap(map); 
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            GameServices.Cleanup();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
      
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
