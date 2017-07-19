using BlankMonoGameTemplate.Engine.Entities;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
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
        public Map()
        {

        }

        public Map(string _name, int _width, int _height, int _tilesize, int _lightLevel = 10, params MapLayer[] _layers)
        {
            Name = _name;
            Width = _width;
            Height = _height;
            Tilesize = _tilesize;
            LightLevel = _lightLevel;

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
        /// Build the map ready for play
        /// </summary>
        public void Build()
        {
            LightMap = new int[Width, Height];
            foreach (var layer in Layers.Values)
            {
                layer.Build(this);
            }
        }

        public void UpdateLights()
        {
            // Set base light level
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    LightMap[x, y] = LightLevel;
                }
            }
        }

        Point ClampCoord(Point _coordinate)
        {
            if (_coordinate.X < 0) _coordinate.X = 0;
            if (_coordinate.Y < 0) _coordinate.Y = 0;
            if (_coordinate.X > Width - 1) _coordinate.X = Width;
            if (_coordinate.Y > Height - 1) _coordinate.Y = Height;
            return _coordinate;
        }

		/// <summary>
		/// Try and add a layer to the map. Returns false if fails.
		/// </summary>
		/// <param name="_key"></param>
		/// <param name="_layer"></param>
		/// <returns></returns>
		internal bool TryAddLayer(string _key, MapLayer _layer)
        {
            if (Layers == null)
            {
                Layers = new Dictionary<string, MapLayer>();
            }
            if (!Layers.ContainsKey(_key) && (_layer.TileIds.Length == Size))
            {
                _layer.Build(this);
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
        /// Get tile at point. If layer isn't specified, return the top layer.
        /// </summary>
        /// <param name="_point"></param>
        /// <param name="_layer"></param>
        /// <returns></returns>
        public Tile GetTileAt(Point _point, string _layer = null)
        {
            return GetTileAt(_point.X, _point.Y, _layer);
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
            Layers[layerKey].TileIds[_x, _y] = _tile.TemplateID;
        }

        [JsonIgnore]
        public int[,] LightMap { get; set; }

        [JsonIgnore]
        public int Size
        {
            get { return Width * Height; }
        }

        [JsonIgnore]
        public List<MapLayer> LayersAsList
        {
            get { return Layers.Values.ToList(); }
        }

        [JsonIgnore]
        public List<Tileset> EmbeddedTilesets
        {
            get
            {
                return Layers.Values.Select(l => l.Tileset).ToList();
            }
        }

        public Vector2 GetPositionAtTile(int x, int y)
        {
            return Layers.ToArray()[0].Value.Tiles[x, y].Position;
        }

        public Tile[] TilesAtWorldPosition(Vector2 _worldPosition)
        {
            var x = (int)_worldPosition.X / Tilesize;
            var y = (int)_worldPosition.Y / Tilesize;
            var tiles = new Tile[Layers.Count];
            var layerKeys = Layers.Keys.ToList();
            for (int i = 0; i < Layers.Count; i++)
            {
                tiles[i] = Layers[layerKeys[i]].Tiles[x, y];
            }
            return tiles;
        }

        #region JSON
        public string Name { get; set; }
        public int Tilesize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int LightLevel { get; set; }
        public Dictionary<string, MapLayer> Layers = new Dictionary<string, MapLayer>();
        #endregion
    }
}
