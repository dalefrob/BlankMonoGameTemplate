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
        #region Static

        public static Map CreateNew(int _width, int _height, int _tileSize, params string[] _layerNames)
        {
            Map map = new Map(16, 16);
            MapTemplate template = new MapTemplate
            {
                Width = _width,
                Height = _height,
                TileSize = _tileSize,
               
            };
            
            return map;
        }

        public static Map FromTemplate(MapTemplate _template)
        {
            Map map = new Map(_template.Width, _template.Height, null);
            foreach (var layerTemplate in _template.LayerTemplates)
            {
                MapLayer newLayer = new MapLayer(map, layerTemplate.Name, 
            }
        }

        #endregion

        public Map(MapTemplate _template)
        {

        }

        public Map(int _width, int _height, params MapLayer[] _layers)
        {
            Width = _width;
            Height = _height;

            if (_layers.Length == 0)
            {
                _layers[0] = new MapLayer(this, "Default", Tileset.GetTileset("Default"), MapLayerType.Tile);
            }

            for (int i = 0; i < _layers.Length; i++)
            {
                TryAddLayer(_layers[i].Name, _layers[i]);
            }
        }

        /// <summary>
        /// Load things like tilesets etc.
        /// </summary>
        public void Load()
        {

        }

        /// <summary>
        /// Try and add a layer to the map. Returns false if fails.
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_array"></param>
        /// <returns></returns>
        internal bool TryAddLayer(string _key, MapLayer _layer)
        {
            if (!layers.ContainsKey(_key) && (_layer.Tiles.Length == Size))
            {
                layers.Add(_key, _layer);
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
                return layers[_layer].Tiles[_x, _y];
            }
            else
            {
                return layers[layers.Keys.Last()].Tiles[_x, _y];
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

        Dictionary<string, MapLayer> layers = new Dictionary<string, MapLayer>();

        public List<Tileset> EmbeddedTilesets
        {
            get
            {
                return layers.Values.Select(l => l.Tileset).ToList();
            }
        }
    }
}
