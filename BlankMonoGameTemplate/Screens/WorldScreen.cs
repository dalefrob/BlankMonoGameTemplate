using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BlankMonoGameTemplate.Engine;
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
        public WorldScreen()
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();

            keyboardListener = new KeyboardListener();
            Textures2D = new Dictionary<string, TextureAtlas>();
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        public override void Activate()
        {
            Game.Window.Title = "Mini Roguelike";
            keyboardListener.KeyReleased += KeyboardListener_KeyReleased;
            base.Activate();
        }

        public override void Deactivate()
        {
            keyboardListener.KeyReleased -= KeyboardListener_KeyReleased;
            base.Deactivate();
        }

        void KeyboardListener_KeyReleased(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.E:
                    var mapEditor = Manager.GetScreen<MapEditorScreen>();
                    mapEditor.Map = World.Map;                 
                    Manager.ChangeScreen<MapEditorScreen>();
                    break;
            }
        }

        public override void LoadContent()
        {

            Console.WriteLine("Load content called");
            // Load all possible assets that will appear on this screen
            // ** WORLD ** //
            var map = Helper.LoadMap(Game.Content, "testmap");
            World = new World(Game, map);
            Tileset.GetTileset("Default");
 
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
            keyboardListener.Update(gameTime);
            World.UpdateWorld(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            World.DrawWorld(gameTime);
            base.Draw(gameTime);
        }

        public MapViewer MapViewer
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

        public void AddTileset(Tileset tileset)
        {
            if (!Tilesets.ContainsKey(tileset.Name))
            {
                Tilesets.Add(tileset.Name, tileset);
            }
        }

        KeyboardListener keyboardListener;
    }
}
