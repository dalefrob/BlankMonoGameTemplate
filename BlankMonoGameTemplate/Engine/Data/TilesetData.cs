using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Data
{
    public class TilesetData
    {
        public TilesetData() 
        {
            Filenames = new Dictionary<int, string>();
            Tiles = new List<Tile>();
        }

        public Dictionary<int, string> Filenames;
        /// <summary>
        /// Name of this tileset
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Properties that correspond to tiles
        /// </summary>
        public List<Tile> Tiles { get; set; }
        /// <summary>
        /// Size of the individual tiles
        /// </summary>
        public int TileSize { get; set; }
    }
}
