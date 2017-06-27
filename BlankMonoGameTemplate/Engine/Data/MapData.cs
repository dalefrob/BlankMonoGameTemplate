using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.TextureAtlases;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework.Content;
using System.Threading.Tasks;
using BlankMonoGameTemplate.Engine.Data;

namespace BlankMonoGameTemplate.Engine
{
    public class MapData
    {
        #region XML

        public MapData() { }
        public MapData(int width, int height, int tileSize, string baseTilesetName) 
        {
            Width = width;
            Height = height;
            TileSize = tileSize;

            Layers.Add(new MapLayer(this, MapLayer.LayerType.Tile) { TilesetName = baseTilesetName });
        }

        public int Width
        {
            get;
            set;
        }
        public int Height 
        {
            get;
            set;
        }

        public int TileSize
        {
            get;
            set;
        }

        public List<MapLayer> Layers = new List<MapLayer>();

        #endregion
       
        #region Methods
   
        public void Jumble(int maxRandomValue) {
            var random = new Random();
            foreach(var layer in Layers) {
                for (int i = 0; i < layer.Tiles.Count(); i++)
                {
                    layer.Tiles[i] = random.Next(maxRandomValue);
                }
            }
        }

        public void SetTileIdAt(int layer, int x, int y, int tileId)
        {
            Layers[layer].Tiles[(y * Width) + x] = tileId;

        }

        public int GetTileIdAt(int layer, int x, int y)
        {
            return Layers[layer].Tiles[(y * Width) + x];
        }

        #endregion
    }
}
