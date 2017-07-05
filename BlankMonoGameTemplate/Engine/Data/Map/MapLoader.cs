using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine
{
    public static class MapLoader
    {
        public static Map FromTemplate(MapTemplate _mapData)
        {
            Map result = new Map(_mapData.Width, _mapData.Height);

            foreach (var layer in _mapData.Layers)
            {
                var tiles = new List<Tile>();
                for (int i = 0; i < layer.TileIds.Count; i++)
                {
                    var tile = new Tile();
                    tile.TemplateID = layer.TileIds[i];
                }
                result.TryAddLayer(layer.Name, ConvertTo2dTileArray(_mapData.Width, _mapData.Height, tiles));
            }

            return result;
        }

        public static Tile[,] ConvertTo2dTileArray(int _width, int _height, List<Tile> _tiles)
        {
            Tile[,] result = new Tile[_width, _height];
            for (int i = 0; i < _tiles.Count; i++)
            {
                var x = i % _width;
                var y = i / _width;
                result[x, y] = _tiles[i];
            }

            return result;
        }
    }
}
