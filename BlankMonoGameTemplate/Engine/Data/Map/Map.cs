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

        public Map(string _name, int _width, int _height, int _tilesize, params MapLayer[] _layers)
        {
            Name = _name;
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
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    LightMap[x, y] = 2;
                }
            }

            var entityManager = GameServices.GetService<EntityManager>();
            var actives = entityManager.ActiveEntities;
            foreach (var e in actives)
            {
                var coord = e.MapCoordinate;
                LightMap[coord.X, coord.Y] = 10;
                var leftCoord = ClampCoord(coord + new Point(-1, 0));
                var upCoord = ClampCoord(coord + new Point(0, -1));
                var downCoord = ClampCoord(coord + new Point(1, 0));
                var rightCoord = ClampCoord(coord + new Point(0, 1));

                var tlCoord = ClampCoord(coord + new Point(-1, -1));
                var trCoord = ClampCoord(coord + new Point(1, -1));
                var blCoord = ClampCoord(coord + new Point(-1, 1));
                var brCoord = ClampCoord(coord + new Point(1, 1));

                LightMap[leftCoord.X, leftCoord.Y] = 7;
                LightMap[upCoord.X, upCoord.Y] = 7;
                LightMap[downCoord.X, downCoord.Y] = 7;
                LightMap[rightCoord.X, rightCoord.Y] = 7;

                LightMap[tlCoord.X, tlCoord.Y] = 4;
                LightMap[trCoord.X, trCoord.Y] = 4;
                LightMap[blCoord.X, blCoord.Y] = 4;
                LightMap[brCoord.X, brCoord.Y] = 4;
            }

        }

        Point ClampCoord(Point _coordinate)
        {
            if (_coordinate.X < 0) _coordinate.X = 0;
            if (_coordinate.Y < 0) _coordinate.Y = 0;
            if (_coordinate.X > Width) _coordinate.X = Width;
            if (_coordinate.Y > Height) _coordinate.Y = Height;
            return _coordinate;
        }

        /// <summary>
        /// Try and add a layer to the map. Returns false if fails.
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_array"></param>
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

        #region JSON
        public string Name { get; set; }
        public int Tilesize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Dictionary<string, MapLayer> Layers = new Dictionary<string, MapLayer>();
        #endregion
    }
}
