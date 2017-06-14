using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace BlankMonoGameTemplate.Engine
{
    public class GameMap
    {
        public GameMap(int width, int height, Tileset defaultTileset)
        {
            Width = width;
            Height = height;
            _layers.Add(new MapLayer(this, defaultTileset));           
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
        }

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
