using System;
using System.Linq;
using BlankMonoGameTemplate.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

namespace BlankMonoGameTemplate.Screens
{
    public class MapEditorScreen : Screen
    {
        public MapEditorScreen(Game game)
        {
            Game = game;
            var screenComponent = (ScreenComponent)Game.Components.Where(c => c.GetType() == typeof(ScreenComponent)).FirstOrDefault();
            spriteBatch = new SpriteBatch(screenComponent.GraphicsDevice);
        }

        public override void Update(GameTime gameTime)
        {
            Console.WriteLine("In MAP EDITOR");
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            gameMapViewer.Draw(spriteBatch);
            spriteBatch.End();
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
        SpriteBatch spriteBatch;
    }
}
