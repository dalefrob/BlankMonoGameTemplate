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

namespace BlankMonoGameTemplate
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        public ScreenGameComponent ScreenComponent { get; private set; }
        public InputListenerComponent InputListenerComponent { get; private set; }
        public MouseListener MouseListener { get; private set; }
        public KeyboardListener KeyboardListener { get; private set; }

        // Screens
        WorldScreen mainScreen;

        public static SpriteFont Mainfont;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = @"Content";

            IsMouseVisible = true;
            IsFixedTimeStep = false;

            // Create components
            ScreenComponent = new ScreenGameComponent(this);
            Components.Add(ScreenComponent);

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
            //CreateTestData();

            mainScreen = new WorldScreen(this);
            var mapEditor = new MapEditorScreen(this);
            //mapEditor.LoadMap("testmap");
            ScreenComponent.Register(mainScreen);
            //ScreenComponent.Register(mapEditor);
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

        
        void CreateTestData()
        {
            var tsFloorData = new TilesetData()
            {
                 Name = "Floor",
                 ImageFilename = "Floor",
                 TileSize = 16
            };
            //Helper.SaveTilesetData(tsFloorData, "Floor");
            var tsWallData = new TilesetData()
            {
                Name = "Wall",
                ImageFilename = "Wall",
                TileSize = 16
            };
            //Helper.SaveTilesetData(tsWallData, "Wall");

            var map = new MapData(24, 24, 16, "Floor");
            map.Layers.Add(new MapLayer(map, MapLayer.LayerType.Tile) { TilesetName = "Wall" });
            map.Jumble(50);
            Helper.SaveMapData(map, "testmap");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
