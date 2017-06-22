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

namespace BlankMonoGameTemplate.Engine
{
    public class Map
    {
        #region XML

        public Map() { }
        public Map(int width, int height, int tileSize, string baseTilesetName) 
        {
            Width = width;
            Height = height;
            TileSize = tileSize;

            Layers.Add(new MapLayer(this) { TilesetName = baseTilesetName });
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

        public List<MapLayer> Layers = new List<MapLayer>();

        [XmlIgnore]
        public bool[,] Collisions
        {
            get;
            private set;
        }

        #endregion

        [XmlIgnore]
        public Dictionary<string, Tileset> Tilesets = new Dictionary<string, Tileset>();

        #region Methods
        public void RegenerateCollisions() 
        {
            Collisions = new bool[Width, Height];
            // Generate collision data
            for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					foreach (var layer in Layers)
					{
                        var index = (y * Width) + x;
						var tile = Tilesets[layer.TilesetName].GetTile(layer.Tiles[index]);
						if (tile.TileFlags.HasFlag(TileFlags.Solid))
						{
							Collisions[x, y] = true;
						}
					}
				}
			}
        }

        public void Jumble(int maxRandomValue) {
            var random = new Random();
            foreach(var layer in Layers) {
                for (int i = 0; i < layer.Tiles.Count(); i++)
                {
                    layer.Tiles[i] = random.Next(maxRandomValue);
                }
            }
        }


        public void SetTileAt(int layer, int x, int y, int tileId)
        {
            Layers[layer].Tiles[(y * Width) + x] = tileId;
        }

        public int GetTileAt(int layer, int x, int y)
        {
            return Layers[layer].Tiles[(y * Width) + x];
        }

        #endregion
        #region Static

        public static void SaveToFile(Map gameMap, string filename)
        {
            XmlSerializer x = new XmlSerializer(gameMap.GetType());
            StreamWriter writer = new StreamWriter(filename);

            x.Serialize(writer, gameMap);
        }

        public static Map LoadFromFile(ContentManager content, string filename)
        {
            Map _map;
            try
            {
				XmlSerializer x = new XmlSerializer(typeof(Map));
				StreamReader reader = new StreamReader(filename);
				_map = (Map)x.Deserialize(reader); 
            } catch (Exception ex) {
                _map = new Map(10, 10, 16, "Floor");
                _map.Layers.Add(new MapLayer(_map) { TilesetName = "Wall" });
            }

			// Also load tilesets
			foreach (var layer in _map.Layers)
			{
				// Load layer tilesets
				var tilesetFilename = layer.TilesetName;
				if (!_map.Tilesets.ContainsKey(tilesetFilename))
				{
					_map.Tilesets.Add(tilesetFilename, Tileset.LoadFromFile(content, tilesetFilename + ".xml"));
				}
			}

            _map.RegenerateCollisions();

            return _map;
        }

        #endregion
    }
}
