using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Data.Map
{
    public enum LayerType
    {
        Texture,
        ObjectID,
        TileFlag,
        Collision,
        Light,
    }

    public class Map
    {
        public static Map CreateNew(string _name, int _width, int _height, int _tileSize, int _numTileLayers = 1)
        {
            Map map = new Map();
            map.Name = _name;
            map.Width = _width;
            map.Height = _height;
            map.Tilesize = _tileSize;

            for (var i = 0; i < _numTileLayers; i++)
            {
                Layer newLayer = Layer.CreateNew(map);
                map.Layers.Add(newLayer);
            }

            return map;
        }

        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Tilesize { get; set; }

        public List<Layer> Layers = new List<Layer>();

        public List<Layer> GetLayersOfType(LayerType _layerType)
        {
            return Layers.Where(l => l.LayerType == _layerType).OrderBy(l => l.LayerID).ToList();
        }
    }

    public class Layer
    {
        public static Layer CreateNew(Map _map, LayerType _layerType = LayerType.Texture)
        {
            Layer layer = new Layer();
            layer.LayerType = _layerType;
            layer.IDArray = new int[_map.Width, _map.Height];
            layer.LayerID = _map.GetLayersOfType(_layerType).Count;
            return layer;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", LayerType.ToString(), LayerID);
        }

        public int LayerID { get; set; }
        public int[,] IDArray { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public LayerType LayerType { get; set; }
    }
   
}
