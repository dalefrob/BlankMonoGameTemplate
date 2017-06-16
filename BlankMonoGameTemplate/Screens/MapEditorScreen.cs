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
using System.Collections.Generic;

namespace BlankMonoGameTemplate.Screens
{
    public class MapEditorScreen : Screen
    {
        public MapEditorScreen(Game game)
        {          
            Game = game;
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            keyboardListener = new KeyboardListener();
            mouseListener = new MouseListener();
            inputListenerComponent = new InputListenerComponent(game, new InputListener[] { keyboardListener, mouseListener });
            mouseListener.MouseMoved += mouseListener_MouseMoved;
            mouseListener.MouseClicked += mouseListener_MouseClicked; 
        }

        #region Events
        void mouseListener_MouseClicked(object sender, MouseEventArgs e)
        {
            var relativeMousePos = Vector2.Subtract(MousePos, tilesetViewer.Position);
            tilesetViewer.SelectedTileCoord = new Vector2(relativeMousePos.X / Map.TileSize, relativeMousePos.Y / Map.TileSize).ToPoint();
            /* // For clicking tiles on the map
            var layerCount = Map.Layers.Count;
            var result = new Tile[layerCount];
            var RelativeMousePos = Vector2.Subtract(MousePos, MapRenderer.Position);
            FocusedTileCoord = new Vector2(RelativeMousePos.X / Map.TileSize, RelativeMousePos.Y / Map.TileSize).ToPoint();
            for (int i = layerCount - 1; i >= 0; i--)
            {
                var tileId = Map.GetTileAt(i, FocusedTileCoord.X, FocusedTileCoord.Y);
                var tile = Tilesets[i].Tiles[tileId];
                result[i] = tile;
            };
             */
        }

        void mouseListener_MouseMoved(object sender, MouseEventArgs e)
        {
            MousePos = e.Position.ToVector2();
        }
        #endregion

        public override void Initialize()
        {         
            // Init events
            mapViewport = Game.GraphicsDevice.Viewport;           

            base.Initialize();
        }

        public override void LoadContent()
        {
            Game.Components.Add(inputListenerComponent);
            keyboardListener.KeyReleased += _keyboardListener_KeyReleased;
            base.LoadContent();
        }

        void _keyboardListener_KeyReleased(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.PageUp:
                    CurrentLayerIndex++;
                    tilesetViewer.Tileset = Tilesets[CurrentLayerIndex];
                    break;
                case Keys.PageDown:
                    CurrentLayerIndex--;
                    tilesetViewer.Tileset = Tilesets[CurrentLayerIndex];
                    break;
                case Keys.Up:
                    FocusedMapCoord += new Point(0, -1);
                    break;
                case Keys.Down:
                    FocusedMapCoord += new Point(0, 1);
                    break;
                case Keys.Left:
                    FocusedMapCoord += new Point(-1, 0);
                    break;
                case Keys.Right:
                    FocusedMapCoord += new Point(1, 0);
                    break;
                case Keys.Enter:
                    var _selTileCoord = tilesetViewer.SelectedTileCoord;
                    MapRenderer.Map.SetTileAt(CurrentLayerIndex, FocusedMapCoord.X, FocusedMapCoord.Y, Tilesets[CurrentLayerIndex].GetTile(_selTileCoord.X, _selTileCoord.Y).TextureId);
                    break;
                case Keys.S:
                    GameMap.SaveToFile(Map, "testmap.xml");
                    break;
            }
        }

        public override void UnloadContent()
        {
            Game.Components.Remove(inputListenerComponent);
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!MapLoaded) return;

            Game.GraphicsDevice.Viewport = mapViewport;
            spriteBatch.Begin();
            MapRenderer.Draw(spriteBatch, FocusedMapCoord);
            var relativeMousePos = Vector2.Subtract(MousePos, MapRenderer.Position);
            if(tilesetViewer.SelectedTileCoord != null) spriteBatch.DrawString(Game1.Mainfont, string.Format("RelMousePos: {0},{1}", relativeMousePos.X, relativeMousePos.Y), new Vector2(216, 0), Color.Blue);
            spriteBatch.DrawString(Game1.Mainfont, string.Format("LastClicked: {0},{1}", FocusedMapCoord.X, FocusedMapCoord.Y), new Vector2(132, 0), Color.Blue);
            spriteBatch.DrawString(Game1.Mainfont, string.Format("RelMousePos: {0},{1}", relativeMousePos.X, relativeMousePos.Y), new Vector2(16, 0), Color.Red);
            spriteBatch.End();

            var textureSheetRect = tilesetViewer.Tileset.TileSheet.Texture.Bounds;
            tileViewport = new Viewport(mapViewport.Width - textureSheetRect.Width, 0, textureSheetRect.Width, textureSheetRect.Height);
            Game.GraphicsDevice.Viewport = tileViewport;
            spriteBatch.Begin();
            tilesetViewer.Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void LoadMap(string filename)
        {
            Map = GameMap.LoadFromFile("testmap.xml");
            foreach (var layer in Map.Layers)
            {
                var tilesetName = layer.TilesetName;
                Tilesets.Add(Tileset.LoadFromFile(Game, tilesetName + ".xml"));
            }

            MapRenderer = new MapRenderer(Game, Map)
            {
                Debug = true,
                Position = new Vector2(16, 16)
            };

            tilesetViewer = new TilesetViewer(spriteBatch, Game.GraphicsDevice, Tilesets[0])
            {
                Position = new Vector2(tileViewport.X, tileViewport.Y)
            };
            MapLoaded = true;
        }

        /// <summary>
        /// The map we're editing
        /// </summary>
        public GameMap Map { get; set; }
        public bool MapLoaded { get; set; }
        /// <summary>
        /// Loaded tilesets
        /// </summary>
        public List<Tileset> Tilesets = new List<Tileset>();

        int _currentLayerIndex = 0;
        public int CurrentLayerIndex
        {
            get { return _currentLayerIndex; }
            set
            {
                _currentLayerIndex = value;
                if (_currentLayerIndex < 0) _currentLayerIndex = Map.Layers.Count - 1;
                if (_currentLayerIndex > Map.Layers.Count - 1) _currentLayerIndex = 0;
            }
        }

        public MapRenderer MapRenderer { get; set; }
        public Point FocusedMapCoord { get; set; }
        public Point SelectedTileCoord { get; set; }

        public Vector2 MousePos { get; private set; }

        public Game Game
        {
            get;
            private set;
        }

        InputListenerComponent inputListenerComponent;
        KeyboardListener keyboardListener;
        MouseListener mouseListener;

        TilesetViewer tilesetViewer;
        SpriteBatch spriteBatch;

        // GUI
        Viewport mapViewport;
        Viewport tileViewport;
    }
}
