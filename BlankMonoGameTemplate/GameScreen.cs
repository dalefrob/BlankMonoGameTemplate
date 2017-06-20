using System;
using Microsoft.Xna.Framework;
using BlankMonoGameTemplate.Engine;
using MonoGame.Extended.Screens;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;

namespace BlankMonoGameTemplate
{
    public class GameScreen : Screen
    {
        public GameScreen(Game1 game)
        {
            Game = game;
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

        }

        public override void Initialize()
        {
            var map = Map.LoadFromFile(Game.Content, "testmap.xml");
            World = new World(Game)
            {
                Map = map
            };
            MapRenderer = new MapRenderer(Game)
            {
                Map = map
			};

			var texture = Game.Content.Load<Texture2D>("Tiles/Objects/Decor1");
			var texAtlas = TextureAtlas.Create("Decor1", texture, 16, 16, int.MaxValue, 0, 0);
            Sprite playerSprite = new Sprite(texAtlas.GetRegion(138)) {
                Origin = Vector2.Zero
            };
			World.Players.Add(new Player(World, true) { Sprite = playerSprite, IsEnabled = true });

            var localPlayer = World.Players.Find(p => p.LocalPlayer == true);
            World.MoveObjectToTile(localPlayer, 3, 1);

            base.Initialize();
        }

        public override void LoadContent()
        {
            
			
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            World.Players.ForEach(p => p.Update(gameTime));
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            MapRenderer.Draw(spriteBatch);
            spriteBatch.End();
            World.Players.ForEach(p => p.Draw(gameTime));
            base.Draw(gameTime);
        }

        public Game1 Game 
        {
            get;
            private set;
        }

        public MapRenderer MapRenderer
        {
            get;
            private set;
        }

        public World World 
        { 
            get;
            private set; 
        }

		SpriteBatch spriteBatch;
    }
}
