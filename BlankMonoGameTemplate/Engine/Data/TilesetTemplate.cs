using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Data
{
    /// <summary>
    /// Tileset specific data for XML import and export
    /// </summary>
    public class TilesetTemplate
    {
        public TilesetTemplate() 
        {
            Filenames = new List<string>();
            TileData = new List<TileTemplate>();
        }

        public List<string> Filenames;
        /// <summary>
        /// Name of this tileset
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Size of the individual tiles
        /// </summary>
        public int TileSize { get; set; }
        /// <summary>
        /// Properties that correspond to tiles
        /// </summary>
        public List<TileTemplate> TileData { get; set; }
    }
}
