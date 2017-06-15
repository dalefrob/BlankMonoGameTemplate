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
    public class GameMap
    {
        public GameMap() { }
        public GameMap(int width, int height, int tileSize, int tileSpacing, string baseTilesetName) 
        {
            Width = width;
            Height = height;
            TileSize = tileSize;
            TileSpacing = tileSpacing;

            Layers.Add(new MapLayer(width, height) { TilesetName = baseTilesetName });
        }

        public int ID
        {
            get;
            set;
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

        public int TileSpacing
        {
            get;
            set;
        }

        List<MapLayer> _layers = new List<MapLayer>();
        public List<MapLayer> Layers
        {
            get { return _layers; }
            set { _layers = value; }
        }

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

        public void SetTileAt(int layer, int x, int y, int id)
        {
            Layers[layer].Tiles[(y * Width) + x] = id;
        }

        public int GetTileAt(int layer, int x, int y)
        {
            return Layers[layer].Tiles[(y * Width) + x];
        }
        #endregion

        #region Static
        public static void SaveToFile(GameMap gameMap, string filename)
        {
            XmlSerializer x = new XmlSerializer(gameMap.GetType());
            StreamWriter writer = new StreamWriter(filename);
            x.Serialize(writer, gameMap);
        }

        public static GameMap LoadFromFile(string filename)
        {
            GameMap result;
            XmlSerializer x = new XmlSerializer(typeof(GameMap));
            StreamReader reader = new StreamReader(filename);
            result = (GameMap)x.Deserialize(reader);
            return result;
        }
        #endregion
    }
}
