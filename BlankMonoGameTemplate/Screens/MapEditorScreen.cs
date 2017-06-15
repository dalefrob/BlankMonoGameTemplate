using System;
using System.Linq;
using BlankMonoGameTemplate.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;

namespace BlankMonoGameTemplate.Screens
{
    public class MapEditorScreen : Screen
    {
        public MapEditorScreen(Game game)
        {
            Game = game;


            // Setup GUI

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            gameMapViewer.Draw(spriteBatch);
            spriteBatch.End();
            tilesetViewer.Draw(gameTime);
            base.Draw(gameTime);
        }

        public override void Initialize()
        {         
            var map = GameMap.LoadFromFile("testmap.xml");
            gameMapViewer = new GameMapRenderer(Game, map)
            {
                Debug = true,
                ScreenPosition = new Vector2(50, 50)
            };
			spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            Tileset tileset = new Tileset(Game.Content.Load<Texture2D>("Tiles/Objects/Floor"), 16, "Floor");
			tilesetViewer = new TilesetViewer(Game.GraphicsDevice, tileset)
			{
				ScreenPosition = new Vector2(400, 10)
			};
            base.Initialize();
        }

        public override void LoadContent()
        {
            
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public Game Game
        {
            get;
            private set;
        }

        GameMapRenderer gameMapViewer;
        TilesetViewer tilesetViewer;
        SpriteBatch spriteBatch;

        // GUI

    }
}
