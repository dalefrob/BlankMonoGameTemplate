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
    public class GameMapViewer
    {
        public GameMapViewer(Game game, GameMap map)
        {
            _gameRef = game;
            Map = map;

            var inputListener = (InputListenerComponent)_gameRef.Components.Where(c => c.GetType() == typeof(InputListenerComponent)).First();
            var mouseListener = (MouseListener)inputListener.Listeners.First(l => l.GetType() == typeof(MouseListener));
            mouseListener.MouseMoved += mouseListener_MouseMoved;
            mouseListener.MouseClicked += mouseListener_MouseClicked;
        }

        public void Initialize()
        {
            foreach (var layer in Map.Layers)
            {
                // Load layer tilesets
                layer.LoadTileset();
            }
        }

        public void Update(GameTime gameTime) 
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < Map.Height; i++) // X coord
            {
                for (var j = 0; j < Map.Width; j++) // Y coord
                {
                    for (int l = 0; l < Map.Layers.Count; l++) // Layer
                    {
                        var tileId = Map.GetTileAt(l, i, j);
                        var tileTexture = Map.Layers[l].Tileset.GetTileTexture(tileId);
                        spriteBatch.Draw(tileTexture, new Rectangle(new Point(Map.TileSize * i, Map.TileSize * j), new Point(Map.TileSize, Map.TileSize)), Color.White);
                    };
                }
            }
            if (Debug)
            {
                spriteBatch.DrawString(Game1.Mainfont, string.Format("MousePos: {0},{1}", MousePos.X, MousePos.Y), new Vector2(400, 10), Color.Red);
                spriteBatch.DrawString(Game1.Mainfont, string.Format("MouseTile: {0},{1}", MousePos.X / Map.TileSize, MousePos.Y / Map.TileSize), new Vector2(400, 20), Color.Blue);
                if (SelectedTileStack != null)
                {
                    var startY = 50;
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
            for (int i = 0; i < Map.Layers.Count; i++)
            {
                var tileId = Map.GetTileAt(i, MousePos.X / Map.TileSize, MousePos.Y / Map.TileSize);
                var tile = Map.Layers[i].Tileset.Tiles[tileId];
                result.Push(tile);
            };
            SelectedTileStack = result.Count > 0 ? result.ToArray() : null;
        }

        void mouseListener_MouseMoved(object sender, MouseEventArgs e)
        {
            MousePos = e.Position;
        }
        #endregion

        Game _gameRef;
        public GameMap Map
        {
            get;
            set;
        }

        public Point MousePos { get; private set; }
        public Tile[] SelectedTileStack { get; set; }
        public bool Debug { get; set; }   
    }
}
