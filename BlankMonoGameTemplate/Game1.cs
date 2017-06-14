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

namespace BlankMonoGameTemplate
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		Dictionary<string, Texture2D> tilesetTextures = new Dictionary<string, Texture2D>();
		Dictionary<string, TextureAtlas> tileSets = new Dictionary<string, TextureAtlas>();


		ScreenComponent screenComponent;
		GameMap gameMap;
        
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
            tilesetTextures.Add("Floor", Content.Load<Texture2D>("Tiles/Objects/Floor"));
            tilesetTextures.Add("Wall", Content.Load<Texture2D>("Tiles/Objects/Wall"));
            tileSets.Add("Floor", TextureAtlas.Create("Floor", tilesetTextures["Floor"], 16, 16, int.MaxValue, 0, 0));
            tileSets.Add("Wall", TextureAtlas.Create("Wall", tilesetTextures["Wall"], 16, 16, int.MaxValue, 0, 0));
            gameMap = new GameMap(36, 36, new Tileset(tileSets["Floor"], 16)) {
                TileSize = 16
            };
            gameMap.Layers[0].FloodWithTileId(148);
            gameMap.AddLayer(new Tileset(tileSets["Wall"], 16));
            gameMap.Layers[1].FloodWithTileId(19);
            gameMap.Layers[1].TileIds[4, 4] = 80;
                
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
            gameMap.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
