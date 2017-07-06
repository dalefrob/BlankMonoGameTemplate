using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine
{
    public class LayerTemplate
    {
        public string Name { get; set; }
        public MapLayerType TypeOfLayer = MapLayerType.Tile;
        public string TilesetName = "Default";
        public List<int> TileTemplateIds = new List<int>();

        public LayerTemplate() { }

        public LayerTemplate(int _width, int _height, MapLayer _layer)
        {
            Name = _layer.Name;
            TypeOfLayer = _layer.TypeOfLayer;
            TilesetName = _layer.TilesetName;

            for (int i = 0; i < _width * _height; i++)
            {
                TileTemplateIds.Add(0);
            }
        }
    }
}
