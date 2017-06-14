using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.TextureAtlases;

namespace BlankMonoGameTemplate.Engine
{
    public class GameMap
    {
        public GameMap(Game game, int width, int height, Tileset defaultTileset)
        {
            _gameRef = game;
            Width = width;
            Height = height;
            _layers.Add(new MapLayer(this, defaultTileset));

            var inputListener = (InputListenerComponent)_gameRef.Components.Where(c => c.GetType() == typeof(InputListenerComponent)).First();
            var mouseListener = (MouseListener)inputListener.Listeners.First(l => l.GetType() == typeof(MouseListener));
            mouseListener.MouseMoved += mouseListener_MouseMoved;
        }

        void mouseListener_MouseMoved(object sender, MouseEventArgs e)
        {
            MousePos = e.Position;
        }

        public void Update(GameTime gameTime) 
        {
            if(Debug) {
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    foreach(MapLayer layer in _layers) {
                        var tileTexture = layer.GetTile(i, j).Texture;
                        spriteBatch.Draw(tileTexture, new Rectangle(new Point(TileSize * i, TileSize * j), new Point(TileSize, TileSize)), Color.White);
                    }
                }
            }
            spriteBatch.DrawString(Game1.Mainfont, string.Format("MousePos: {0},{1}", MousePos.X, MousePos.Y), new Vector2(10,10), Color.Red);
            spriteBatch.DrawString(Game1.Mainfont, string.Format("MouseTile: {0},{1}", MousePos.X / TileSize, MousePos.Y / TileSize), new Vector2(10, 30), Color.Black);
        }

        Game _gameRef;

        public Point MousePos { get; private set; }
        public bool Debug { get; set; }

        public int Width
        {
            get;
            private set;
        }
        public int Height 
        {
            get;
            private set;
        }

        public void AddLayer(Tileset tileset) {
            _layers.Add(new MapLayer(this, tileset));
        }

        List<MapLayer> _layers = new List<MapLayer>();
        public List<MapLayer> Layers {
            get { return _layers; }
        }

        public int TileSize
        {
            get;
            set;
        }

        public int TileSpacing 
        {
            get;
            set;
        }

        public static void SaveToFile(string filename) {
            
        }

        public static GameMap LoadFromFile(string filename) 
        {
            throw new NotImplementedException();
        }
    }
}
