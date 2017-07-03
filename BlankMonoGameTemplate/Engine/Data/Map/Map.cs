using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine
{
    /// <summary>
    /// Game map.
    /// </summary>
    public class Map
    {
        public Map(int _width, int _height, params string[] _layerNames)
        {
            Width = _width;
            Height = _height;

            for (int i = 0; i < _layerNames.Length; i++)
            {
                var tileArray = new Tile[Width, Height];
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        tileArray[x, y] = Tile.Default();
                    }
                }
                TryAddLayer(_layerNames[i], tileArray);
            }
        }

        /// <summary>
        /// Try and add a layer to the map. Returns false if fails.
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_array"></param>
        /// <returns></returns>
        internal bool TryAddLayer(string _key, Tile[,] _array)
        {
            if (!layers.ContainsKey(_key) && (_array.Length == Size))
            {               
                layers.Add(_key, _array);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get tile at point. If layer isn't specified, return the top layer.
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_layer"></param>
        /// <returns></returns>
        public Tile GetTileAt(int _x, int _y, string _layer = null)
        {
            if (_layer != null)
            {
                return layers[_layer][_x, _y];
            }
            else
            {
                return layers[layers.Keys.Last()][_x, _y];
            }
        }

        /// <summary>
        /// Get tile at point. If layer isn't specified, return the top layer.
        /// </summary>
        /// <param name="_point"></param>
        /// <param name="_layer"></param>
        /// <returns></returns>
        public Tile GetTileAt(Point _point, string _layer = null)
        {
            return GetTileAt(_point.X, _point.Y, _layer);
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Size
        {
            get { return Width * Height; }
        }

        Dictionary<string, Tile[,]> layers = new Dictionary<string, Tile[,]>();
        Dictionary<string, Tileset> tilesets = new Dictionary<string, Tileset>();

        public Dictionary<string, Tileset> EmbeddedTilesets
        {
            get
            {
                return tilesets;
            }
        }
    }
}
