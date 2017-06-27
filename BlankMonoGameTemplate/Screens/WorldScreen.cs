using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BlankMonoGameTemplate.Engine;
using MonoGame.Extended.Screens;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Input.InputListeners;
using BlankMonoGameTemplate.Screens;
using System.Collections.Generic;
using BlankMonoGameTemplate.Engine.Data;

namespace BlankMonoGameTemplate
{
    public class WorldScreen : Screen
    {
        public WorldScreen(Game1 game)
        {
            Game = game;
            Textures2D = new Dictionary<string, TextureAtlas>();
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        void keyboardListener_KeyTyped(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.E:
                    this.IsVisible = false;
                    this.FindScreen<MapEditorScreen>().Show();
                    break;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            Console.WriteLine("Init called");

            var map = Helper.LoadMapData(Game.Content, "testmap");
            World = new World(Game, map);

            Game.KeyboardListener.KeyTyped += keyboardListener_KeyTyped;

 
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Console.WriteLine("Load content called");
            // Load all possible assets that will appear on this screen
            // ** TILESETS ** //
            AddTileset("Floor", Helper.LoadTilesetData("Floor"));
            AddTileset("Wall", Helper.LoadTilesetData("Wall"));
            // ** ENTITIES ** //
            AddTexture2D("GUI0", Game.Content.Load<Texture2D>("Tiles/GUI/GUI0"));
            AddTexture2D("Player0", Game.Content.Load<Texture2D>("Tiles/Characters/Player0"));
            AddTexture2D("Player1", Game.Content.Load<Texture2D>("Tiles/Characters/Player1"));
            AddTexture2D("Slime0", Game.Content.Load<Texture2D>("Tiles/Characters/Slime0"));
            AddTexture2D("Slime1", Game.Content.Load<Texture2D>("Tiles/Characters/Slime1"));
            AddTexture2D("Door0", Game.Content.Load<Texture2D>("Tiles/Objects/Door0"));
            AddTexture2D("Door1", Game.Content.Load<Texture2D>("Tiles/Objects/Door1"));
            AddTexture2D("Key", Game.Content.Load<Texture2D>("Tiles/Items/Key"));

            World.Initialize();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            World.UpdateWorld(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            World.DrawWorld(gameTime);
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

        public static Dictionary<string, TextureAtlas> Textures2D { get; private set; }
        public static Dictionary<string, Tileset> Tilesets = new Dictionary<string, Tileset>();

        public void AddTexture2D(string name, Texture2D texture)
        {         
            if (!Textures2D.ContainsKey(name))
            {
                var texAtlas = TextureAtlas.Create(name, texture, 16, 16, int.MaxValue);
                Textures2D.Add(name, texAtlas);
            }
        }

        public void AddTileset(string name, TilesetData tsData)
        {
            if (!Tilesets.ContainsKey(name))
            {
                var tset = new Tileset(Game.Content, tsData.Name, tsData);
                Tilesets.Add(name, tset);
            }
        }
    }
}
