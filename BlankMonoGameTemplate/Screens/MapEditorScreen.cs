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
    public class MapEditorScreen : GameScreen
    {
        public MapEditorScreen(Game1 game)
        {          
            Game = game;
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        #region Events
        void mouseListener_MouseClicked(object sender, MouseEventArgs e)
        {
            var relativeMousePos = Vector2.Subtract(MousePos, tilesetViewer.Position);
            tilesetViewer.SelectedTileCoord = new Vector2(relativeMousePos.X / Map.TileSize, relativeMousePos.Y / Map.TileSize).ToPoint();
        }

        void mouseListener_MouseMoved(object sender, MouseEventArgs e)
        {
            MousePos = e.Position.ToVector2();
        }
        #endregion

        public override void OnShow()
        {

            base.OnShow();
        }

        public override void Initialize()
        {
            Game.MouseListener.MouseMoved += mouseListener_MouseMoved;
            Game.MouseListener.MouseClicked += mouseListener_MouseClicked;
            base.Initialize();
        }

        public override void LoadContent()
        {
            selectionTexture = Game.Content.Load<Texture2D>("UI/Selection");

			base.LoadContent();
        }

        void keyboardListener_KeyTyped(object sender, KeyboardEventArgs e)
        {
            if(e.Character.HasValue) {
                if(char.IsNumber(e.Character.Value)) {
                    int num = Int32.Parse(e.Character.Value.ToString());
                    if(num < Map.Layers.Count) {
                        CurrentLayerIndex = num;
                    }
                }
            }

            var _selTileCoord = tilesetViewer.SelectedTileCoord;

            switch (e.Key)
            {
                case Keys.PageUp:
                    CurrentLayerIndex++;
                    break;
                case Keys.PageDown:
                    CurrentLayerIndex--;
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
                    MapRenderer.Map.SetTileAt(CurrentLayerIndex, FocusedMapCoord.X, FocusedMapCoord.Y, Tilesets[CurrentLayerIndex].GetTile(_selTileCoord.X, _selTileCoord.Y).TextureId);
                    break;
                case Keys.S:
                    Map.SaveToFile(Map, "testmap.xml");
                    Tileset.SaveToFile(Tilesets[CurrentLayerIndex], Tilesets[CurrentLayerIndex].TextureSheetName + ".xml");
                    break;
                case Keys.F:
                    FloodLayer();
                    break;
                case Keys.C:
                    var tile = Tilesets[CurrentLayerIndex].GetTile(_selTileCoord.X, _selTileCoord.Y);
                    tile.TileFlags |= TileFlags.Solid;
                    break;
            }
        }

		public void FloodLayer()
		{
            var _selTileCoord = tilesetViewer.SelectedTileCoord;
            int tileId = Tilesets[CurrentLayerIndex].GetTile(_selTileCoord.X, _selTileCoord.Y).TextureId;
            for (int i = 0; i < Map.Width * Map.Height; i++) {
				Map.Layers[CurrentLayerIndex].Tiles[i] = tileId;
            }		
		}

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.CornflowerBlue);
            if (!MapLoaded) return;

            spriteBatch.Begin();
            MapRenderer.Draw(spriteBatch);
            spriteBatch.Draw(selectionTexture, MapRenderer.Position + (FocusedMapCoord.ToVector2() * Map.TileSize), Color.White);

            var relativeMousePos = Vector2.Subtract(MousePos, MapRenderer.Position);
            spriteBatch.DrawString(Game1.Mainfont, string.Format("RelMousePos: {0},{1}", relativeMousePos.X, relativeMousePos.Y), new Vector2(216, 0), Color.Blue);
            spriteBatch.DrawString(Game1.Mainfont, string.Format("MapTile: {0},{1}", FocusedMapCoord.X, FocusedMapCoord.Y), new Vector2(132, 0), Color.Blue);
            spriteBatch.DrawString(Game1.Mainfont, string.Format("RelMousePos: {0},{1}", relativeMousePos.X, relativeMousePos.Y), new Vector2(16, 0), Color.Red);

            var textureSheetRect = tilesetViewer.Tileset.TileSheet.Texture.Bounds;
            tilesetViewer.Draw(gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void LoadMap(string filename)
        {
            Map = Map.LoadFromFile(Game.Content, "testmap.xml");
            foreach (var layer in Map.Layers)
            {
                var tilesetName = layer.TilesetName;
                Tilesets.Add(Tileset.LoadFromFile(Game.Content, tilesetName + ".xml"));
            }

            MapRenderer = new MapRenderer(Game, Map)
            {
                Debug = true,
                Position = new Vector2(16, 16)
            };

            tilesetViewer = new TilesetViewer(spriteBatch, Game.GraphicsDevice, Tilesets[0])
            {
                Position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2, 0)
            };
            MapLoaded = true;
        }

        /// <summary>
        /// The map we're editing
        /// </summary>
        public Map Map { get; set; }
        public bool MapLoaded { get; set; }
        /// <summary>
        /// Loaded tilesets
        /// </summary>
        public List<Tileset> Tilesets = new List<Tileset>();

        int _currentLayerIndex;
        public int CurrentLayerIndex
        {
            get { return _currentLayerIndex; }
            set
            {
                _currentLayerIndex = value;
                if (_currentLayerIndex < 0) _currentLayerIndex = Map.Layers.Count - 1;
                if (_currentLayerIndex > Map.Layers.Count - 1) _currentLayerIndex = 0;
                tilesetViewer.Tileset = Tilesets[CurrentLayerIndex];
            }
        }

        public MapRenderer MapRenderer { get; set; }
        Point _focusedMapCoord = Point.Zero;
        public Point FocusedMapCoord 
        { 
            get { return _focusedMapCoord; }
            set {
                int x = 0;
                int y = 0;
                if (value.X >= 0 && value.X < Map.Width) x = value.X;
                if (value.Y >= 0 && value.Y < Map.Height) y = value.Y;
                _focusedMapCoord = new Point(x, y);
            }
        }
        public Point SelectedTileCoord { get; set; }

        public Vector2 MousePos { get; private set; }

        public Game1 Game
        {
            get;
            private set;
        }

        KeyboardListener keyboardListener;
        MouseListener mouseListener;

        TilesetViewer tilesetViewer;
        SpriteBatch spriteBatch;

        // GUI
        Texture2D selectionTexture;

    }
}
