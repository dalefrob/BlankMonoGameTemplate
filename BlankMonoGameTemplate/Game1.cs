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

namespace BlankMonoGameTemplate
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		//Dictionary<string, Texture2D> tilesetTextures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Tileset> Tilesets = new Dictionary<string, Tileset>();

		ScreenComponent screenComponent;
		GameMapViewer gameMapViewer;

        public static SpriteFont Mainfont;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = @"Content";

            IsMouseVisible = true;
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
            var inputListenerComponent = new InputListenerComponent(this);
            inputListenerComponent.Listeners.Add(new MouseListener());
            Components.Add(inputListenerComponent);
            screenComponent = new ScreenComponent(this);
            Components.Add(screenComponent);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Mainfont = Content.Load<SpriteFont>("Fonts/mainfont");
            //tilesetTextures.Add("Floor", Content.Load<Texture2D>("Tiles/Objects/Floor"));
            //tilesetTextures.Add("Wall", Content.Load<Texture2D>("Tiles/Objects/Wall"));
            //tileSets.Add("Floor", TextureAtlas.Create("Floor", tilesetTextures["Floor"], 16, 16, int.MaxValue, 0, 0));
            //tileSets.Add("Wall", TextureAtlas.Create("Wall", tilesetTextures["Wall"], 16, 16, int.MaxValue, 0, 0));
            //var map = new GameMap(24, 24, 16, 0);
            var tileset = Tileset.LoadFromFile(this, "tileset_floor.xml");
            Tilesets.Add(tileset.TextureSheetName, tileset);
            var map = GameMap.LoadFromFile("testmap.xml");
            gameMapViewer = new GameMapViewer(this, map, Tilesets[map.Layers[0].TilesetName]) {
                Debug = true
            };
            /*
            gameMapViewer.Layers[0].FloodWithTileId(148);
            gameMapViewer.AddLayer(new Tileset(tileSets["Wall"], 16));
            gameMapViewer.Layers[1].FloodWithTileId(19);
            gameMapViewer.Layers[1].TileIdArray[4, 4] = 80;
             * */
   
        }

        private void CreateTestMap()
        {
            var map = new GameMap(24, 24, 16, 0, "Floor");
            map.Layers.Add(new MapLayer(24, 24) { TilesetName = "Wall" });
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
            spriteBatch.Begin();
            gameMapViewer.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
