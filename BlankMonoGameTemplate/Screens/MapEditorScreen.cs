﻿﻿using System;
using System.Linq;
using BlankMonoGameTemplate.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
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
        public MapEditorScreen() { }

        #region Events
        void MouseUp_MouseClicked(object sender, MouseEventArgs e)
        {
            tilesetViewer.SelectTileSlotFromPosition(e.Position.ToVector2());
        }

        void MouseListener_MouseMoved(object sender, MouseEventArgs e)
        {
            MousePos = e.Position.ToVector2();
        }
        #endregion

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Manager.GraphicsDevice);
            mapViewer = new MapViewer
            {
                Position = new Vector2(0, 16)
            };
            tilesetViewer = new TilesetViewer
            {
                Position = new Vector2(416, 0)
            };
            keyboardListener = new KeyboardListener();
            mouseListener = new MouseListener();

            base.Initialize();
        }

        public override void Activate()
        {
            Game.Window.Title = "Map Editor";
            keyboardListener.KeyReleased += KeyboardListener_KeyReleased;
            mouseListener.MouseMoved += MouseListener_MouseMoved;
            mouseListener.MouseUp += MouseUp_MouseClicked;
            base.Activate();
        }

        public override void Deactivate()
        {
            keyboardListener.KeyReleased -= KeyboardListener_KeyReleased;
			mouseListener.MouseMoved -= MouseListener_MouseMoved;
			mouseListener.MouseClicked -= MouseUp_MouseClicked;
            base.Deactivate();
        }

        public override void LoadContent()
        {
            selectionTexture = Game.Content.Load<Texture2D>("UI/Selection");

			base.LoadContent();
        }

        void KeyboardListener_KeyReleased(object sender, KeyboardEventArgs e)
        {
            if (e.Character.HasValue)
            {
                if (char.IsNumber(e.Character.Value))
                {
                    int num = Int32.Parse(e.Character.Value.ToString());
                    if (num < Map.Layers.Count)
                    {
                        CurrentLayerIndex = num;
                    }
                }
            }           

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
                    Map.SetTileIdAt(CurrentLayerIndex, FocusedMapCoord.X, FocusedMapCoord.Y, tilesetViewer.SelectedTileSlot.Tile.TemplateID);
                    break;
                case Keys.S:
                    Helper.SaveMapData(Map, "testmap");
                    break;
                case Keys.T:
                    //Helper.SaveTilesetData(CurrentLayerTileset.Data, CurrentLayerTileset.Data.Name);
                    break;
                case Keys.F:
                    FloodLayer();
                    break;
                case Keys.C:
                    tilesetViewer.SelectedTileSlot.Tile.Obstacle = !tilesetViewer.SelectedTileSlot.Tile.Obstacle;
                    //tile.Obstacle = !tile.Obstacle;
                    break;
                case Keys.Escape:
                    Manager.ChangeScreen<WorldScreen>();
                    break;
            }
        }

		public void FloodLayer()
		{
            int tileId = tilesetViewer.SelectedTileSlot.Tile.TemplateID + 1;
            for (int i = 0; i < Map.Width * Map.Height; i++) {
				Map.Layers[CurrentLayerIndex].TileIds[i] = tileId;
            }		
		}

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            mouseListener.Update(gameTime);
            keyboardListener.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.Clear(Color.CornflowerBlue);
            if (!IsMapLoaded) return;
            
            spriteBatch.Begin();
            mapViewer.Draw(Map, gameTime);
            spriteBatch.Draw(selectionTexture, mapViewer.Position + (FocusedMapCoord.ToVector2() * Map.TileSize), Color.White);

            var relativeMousePos = Vector2.Subtract(MousePos, mapViewer.Position);
            spriteBatch.DrawString(Game1.Mainfont, string.Format("RelMousePos: {0},{1}", relativeMousePos.X, relativeMousePos.Y), new Vector2(216, 0), Color.Blue);
            spriteBatch.DrawString(Game1.Mainfont, string.Format("MapTile: {0},{1}", FocusedMapCoord.X, FocusedMapCoord.Y), new Vector2(132, 0), Color.Blue);
            spriteBatch.DrawString(Game1.Mainfont, string.Format("RelMousePos: {0},{1}", relativeMousePos.X, relativeMousePos.Y), new Vector2(16, 0), Color.Red);

            //var tile = WorldScreen.Tilesets[Map.Layers[CurrentLayerIndex].TilesetName].GetTileData(tilesetViewer.SelectedTileCoord.X, tilesetViewer.SelectedTileCoord.Y);
            //spriteBatch.DrawString(Game1.Mainfont, string.Format("Obstacle:{0}", tile.Obstacle), new Vector2(316, 0), Color.Red);

            tilesetViewer.Draw(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        MapTemplate _map;
        public MapTemplate Map {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
                // Load the first later tileset
                tilesetViewer.Tileset = Tileset.GetTileset(value.Layers[0].TilesetName);
            }
        }

        public bool IsMapLoaded
        {
            get { return (Map != null); }
        }

        int _currentLayerIndex;
        public int CurrentLayerIndex
        {
            get { return _currentLayerIndex; }
            set
            {
                var _val = value;

                if (_val < 0) _val = Map.Layers.Count - 1;
                if (_val > Map.Layers.Count - 1) _val = 0;
                _currentLayerIndex = _val;
            }
        }

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

        public TileSlot SelectedTileSlot { get; set; }

        public Vector2 MousePos { get; private set; }

        KeyboardListener keyboardListener;
        MouseListener mouseListener;
        MapViewer mapViewer;
        TilesetViewer tilesetViewer;
        SpriteBatch spriteBatch;

        // GUI
        Texture2D selectionTexture;

    }
}
