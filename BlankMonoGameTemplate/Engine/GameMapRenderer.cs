using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.TextureAtlases;
using System.Xml.Serialization;
using System.IO;

namespace BlankMonoGameTemplate.Engine
{
    public class GameMapRenderer
    {
        public GameMapRenderer(Game game, GameMap map)
        {
            _gameRef = game;
            ScreenPosition = Vector2.Zero;
            Map = map;

            var inputListener = (InputListenerComponent)_gameRef.Components.Where(c => c.GetType() == typeof(InputListenerComponent)).First();
            var mouseListener = (MouseListener)inputListener.Listeners.First(l => l.GetType() == typeof(MouseListener));
            mouseListener.MouseMoved += mouseListener_MouseMoved;
            mouseListener.MouseClicked += mouseListener_MouseClicked;

            Initialize();
        }

        public void Initialize()
        {
            foreach (var layer in Map.Layers)
            {
                // Load layer tilesets
                var tilesetFilename = layer.TilesetName;
                if(!TilesetStore.ContainsKey(tilesetFilename)) {
                    TilesetStore.Add(tilesetFilename, Tileset.LoadFromFile(_gameRef, tilesetFilename + ".xml"));
                }
            }
        }

        public void Update(GameTime gameTime) 
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
			for (int l = 0; l < Map.Layers.Count; l++) // Layer
			{
	            for (var i = 0; i < Map.Height; i++) // X coord
	            {
	                for (var j = 0; j < Map.Width; j++) // Y coord
	                {	                    
	                    var tileId = Map.GetTileAt(l, i, j);
	                    var tileTexture = TilesetStore[Map.Layers[l].TilesetName].GetTileTexture(tileId);
                        var color = Equals(new Point(i, j), LastClickedTileCoords) ? Color.Red : Color.White;
                        var destinationRect = new Rectangle
                        {
                            X = (int)ScreenPosition.X + Map.TileSize * i,
                            Y = (int)ScreenPosition.Y + Map.TileSize * j,
                            Width = Map.TileSize,
                            Height = Map.TileSize
                        };
	                    spriteBatch.Draw(tileTexture, destinationRect, color);	                    
	                }
	            }
            };
            if (Debug)
            {
                spriteBatch.DrawString(Game1.Mainfont, string.Format("RelMousePos: {0},{1}", RelativeMousePosition.X, RelativeMousePosition.Y), new Vector2(400, 10), Color.Red);
                spriteBatch.DrawString(Game1.Mainfont, string.Format("LastClicked: {0},{1}", LastClickedTileCoords.X, LastClickedTileCoords.Y), new Vector2(400, 20), Color.Blue);

                if (SelectedTileStack != null)
                {
                    var startY = 60;
                    spriteBatch.DrawString(Game1.Mainfont, string.Format("TileFlags: {0}", NetType(LastClickedTileCoords.X, LastClickedTileCoords.Y)), new Vector2(400, 50), Color.Yellow);
                    for (int i = 0; i < SelectedTileStack.Count(); i++)
                    {
                        spriteBatch.DrawString(Game1.Mainfont, SelectedTileStack[i].ToString(), new Vector2(400, startY + (i * 40)), Color.White);
                    }
                }
            }
        }

        #region Events
        void mouseListener_MouseClicked(object sender, MouseEventArgs e)
        {
            var result = new Stack<Tile>();
            LastClickedTileCoords = new Vector2(RelativeMousePosition.X / Map.TileSize, RelativeMousePosition.Y / Map.TileSize).ToPoint();
            for (int i = 0; i < Map.Layers.Count; i++)
            {
                var tileId = Map.GetTileAt(i, LastClickedTileCoords.X, LastClickedTileCoords.Y);
                var tile = TilesetStore[Map.Layers[i].TilesetName].Tiles[tileId];
                result.Push(tile);
            };
            SelectedTileStack = result.Count > 0 ? result.ToArray() : null;
        }

        void mouseListener_MouseMoved(object sender, MouseEventArgs e)
        {
            MousePos = e.Position.ToVector2();
        }
        #endregion

        public TileFlags NetType(int x, int y) {
            var index = (y * Map.Width) + x;
            var result = TileFlags.Walkable;
            foreach(var layer in Map.Layers) {
                var ts = TilesetStore[layer.TilesetName];
                result = result | ts.Tiles[index].Type;
            }
            return result;
        }

        public Vector2 ScreenPosition { get; set; }
        public Vector2 MousePos { get; private set; }
        public Vector2 RelativeMousePosition
        {
            get {
                return Vector2.Subtract(MousePos, ScreenPosition);
            }
        }
        public Point LastClickedTileCoords { get; private set; }
        public Tile[] SelectedTileStack { get; set; }
        public bool Debug { get; set; } 

        Dictionary<string, Tileset> TilesetStore = new Dictionary<string, Tileset>();
        Game _gameRef;
        public GameMap Map
        {
            get;
            set;
        }
          
    }
}
