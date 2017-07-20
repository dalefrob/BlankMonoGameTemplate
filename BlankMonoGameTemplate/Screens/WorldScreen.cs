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
using BlankMonoGameTemplate.Engine.Entities;
using MonoGame.Extended;
using BlankMonoGameTemplate.Engine.Data.Map;

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
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            camera = new Camera2D(Game.GraphicsDevice);

            Managers.Add(new MapManager());

            keyboardListener = new KeyboardListener();
   
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
                    var mapEditor = ScreenManager.GetScreen<MapEditorScreen>();
             
                    ScreenManager.ChangeScreen<MapEditorScreen>();
                    break;
            }
        }

        public override void LoadContent()
        {
            Console.WriteLine("Load content called");
            // Load all possible assets that will appear on this screen
            // ** WORLD ** //
            var mapMgr = GetManager<MapManager>();
            mapMgr.Map = Helper.LoadMap(Game.Content, "newtestmap");
            Tileset.GetTileset("Default");
            mapRenderer = new MapRenderer(mapMgr.Map);
 
            // ** ENTITIES ** //
            /*
            AddTexture2D("GUI0", Game.Content.Load<Texture2D>("Tiles/GUI/GUI0"));
            AddTexture2D("Player0", Game.Content.Load<Texture2D>("Tiles/Characters/Player0"));
            AddTexture2D("Player1", Game.Content.Load<Texture2D>("Tiles/Characters/Player1"));
            AddTexture2D("Slime0", Game.Content.Load<Texture2D>("Tiles/Characters/Slime0"));
            AddTexture2D("Slime1", Game.Content.Load<Texture2D>("Tiles/Characters/Slime1"));
            AddTexture2D("Door0", Game.Content.Load<Texture2D>("Tiles/Objects/Door0"));
            AddTexture2D("Door1", Game.Content.Load<Texture2D>("Tiles/Objects/Door1"));
            AddTexture2D("Key", Game.Content.Load<Texture2D>("Tiles/Items/Key"));
             */

        }

        /// <summary>
        /// This happens after pressing 'X' in the top corner of the window
        /// </summary>
        public override void UnloadContent()
        {
            keyboardListener = null;
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            //keyboardListener.Update(gameTime);
            //entityManager.Update(gameTime);
            mapRenderer.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());
            mapRenderer.Draw(spriteBatch, gameTime);
            //entityManager.Draw(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        KeyboardListener keyboardListener;
		EntitySystem entityManager;
        SpriteBatch spriteBatch;
		MapRenderer mapRenderer;
		public Camera2D camera { get; private set; }
    }
}
