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
        public Map(int _width, int _height, int _tilesize, params MapLayer[] _layers)
        {
            Width = _width;
            Height = _height;
            Tilesize = _tilesize;

            if (_layers.Length == 0)
            {
                TryAddLayer("Default", new MapLayer(_width, _height));
            }

            for (int i = 0; i < _layers.Length; i++)
            {
                TryAddLayer(_layers[i].Name, _layers[i]);
            }
        }

        /// <summary>
        /// Load a map from a template including layers
        /// </summary>
        /// <param name="_template"></param>
        public Map(MapTemplate _template)
        {
            Width = _template.Width;
            Height = _template.Height;
            Tilesize = _template.TileSize;
            foreach (LayerTemplate lTemplate in _template.LayerTemplates)
            {
                MapLayer mapLayer = new MapLayer(Width, Height)
                {
                     Name = lTemplate.Name,
                     TypeOfLayer = lTemplate.TypeOfLayer,
                     Tileset = Tileset.GetTileset(lTemplate.TilesetName),                 
                };

                TryAddLayer(lTemplate.Name, mapLayer);
            }
        }

        /// <summary>
        /// Save a map to a template including layers
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public MapTemplate ToTemplate(string _name)
        {
            MapTemplate mapTemplate = new MapTemplate(_name, Width, Height, Tilesize);
            foreach (MapLayer layer in Layers.Values)
            {
                LayerTemplate lTemplate = new LayerTemplate(Width, Height, layer);
                mapTemplate.LayerTemplates.Add(lTemplate);
            }
            return mapTemplate;
        }

        /// <summary>
        /// Try and add a layer to the map. Returns false if fails.
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_array"></param>
        /// <returns></returns>
        internal bool TryAddLayer(string _key, MapLayer _layer)
        {
            if (!Layers.ContainsKey(_key) && (_layer.Tiles.Length == Size))
            {
                Layers.Add(_key, _layer);
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
                return Layers[_layer].Tiles[_x, _y];
            }
            else
            {
                return Layers[Layers.Keys.Last()].Tiles[_x, _y];
            }
        }

        /// <summary>
        /// Set a tile at a specific map point. No layer? get first.
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_layer"></param>
        /// <returns></returns>
        public void SetTileAt(Tile _tile, int _x, int _y, string _layer = null)
        {
            var keys = Layers.Keys.ToList();
            var layerKey = (_layer == null) ? keys[0] : _layer;
            Layers[layerKey].Tiles[_x, _y] = _tile;
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

        public int Tilesize { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Size
        {
            get { return Width * Height; }
        }

        Dictionary<string, MapLayer> _layers = new Dictionary<string, MapLayer>();
        public Dictionary<string, MapLayer> Layers {
            get { return _layers; }
            internal set { _layers = value; }
        }
        public List<MapLayer> LayersAsList
        {
            get { return Layers.Values.ToList(); }
        }

        public List<Tileset> EmbeddedTilesets
        {
            get
            {
                return Layers.Values.Select(l => l.Tileset).ToList();
            }
        }
    }
}
