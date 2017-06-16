using System;
using System.Linq;
using BlankMonoGameTemplate.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace BlankMonoGameTemplate.Screens
{
    public class MapEditorScreen : Screen
    {
        public MapEditorScreen(Game game)
        {          
            Game = game;
            _keyboardListener = new KeyboardListener();
            _inputListenerComponent = new InputListenerComponent(game, new InputListener[] { _keyboardListener });
        }

        public override void Initialize()
        {         
            var map = GameMap.LoadFromFile("testmap.xml");
            gameMapViewer = new GameMapRenderer(Game, map)
            {
                Debug = true,
                ScreenPosition = new Vector2(16, 16)
            };
			spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            Tileset tileset = new Tileset(Game.Content.Load<Texture2D>("Tiles/Objects/Floor"), 16, "Floor");
            tilesetViewer = new TilesetViewer(spriteBatch, Game.GraphicsDevice, tileset);
            
            // Init events
            mapViewport = Game.GraphicsDevice.Viewport;           

            base.Initialize();
        }

        public override void LoadContent()
        {
            Game.Components.Add(_inputListenerComponent);
            _keyboardListener.KeyReleased += _keyboardListener_KeyReleased;
            tilesetViewer.SelectionChanged += tilesetViewer_SelectionChanged;
            base.LoadContent();
        }

        void tilesetViewer_SelectionChanged(object sender, TileViewerEventArgs e)
        {
            if (gameMapViewer.SelectedTileStack != null)
            {
                gameMapViewer.SelectedTileStack[1] = e.SelectedTile;
                var coords = gameMapViewer.LastClickedTileCoords;
                gameMapViewer.Map.SetTileAt(1, coords.X, coords.Y, e.SelectedTile.TextureId);
            }
        }

        void _keyboardListener_KeyReleased(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.Up:
                    tilesetViewer.SelectedTile += new Point(0, -1);
                    break;
                case Keys.Down:
                    tilesetViewer.SelectedTile += new Point(0, 1);
                    break;
                case Keys.Left:
                    tilesetViewer.SelectedTile += new Point(-1, 0);
                    break;
                case Keys.Right:
                    tilesetViewer.SelectedTile += new Point(1, 0);
                    break;
            }
        }

        public override void UnloadContent()
        {
            Game.Components.Remove(_inputListenerComponent);
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Viewport = mapViewport;
            spriteBatch.Begin();
            gameMapViewer.Draw(spriteBatch);
            spriteBatch.End();

            var textureSheetRect = tilesetViewer.Tileset.TileSheet.Texture.Bounds;
            tilePickerViewport = new Viewport(mapViewport.Width - textureSheetRect.Width, 0, textureSheetRect.Width, textureSheetRect.Height);
            Game.GraphicsDevice.Viewport = tilePickerViewport;
            spriteBatch.Begin();
            tilesetViewer.Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Game Game
        {
            get;
            private set;
        }

        InputListenerComponent _inputListenerComponent;
        KeyboardListener _keyboardListener;

        GameMapRenderer gameMapViewer;
        TilesetViewer tilesetViewer;
        SpriteBatch spriteBatch;

        // GUI
        Viewport mapViewport;
        Viewport tilePickerViewport;
    }
}
