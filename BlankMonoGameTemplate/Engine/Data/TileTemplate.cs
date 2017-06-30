using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Data
{
    /// <summary>
    /// Tile specific data for import and export
    /// </summary>
    [System.Serializable]
    public class TileTemplate
    {
        // Identifying properties
        public int RegionId { get; set; }
        public string AtlasName { get; set; }

        // Game specific properties
        public TileFlags TileFlags { get; set; }
        public bool Obstacle { get; set; }

        public TileTemplate() { }

        public TileTemplate(int regionId, string atlasName)
            : this()
        {
            RegionId = regionId;
            AtlasName = atlasName;
        }
      
        public static TileTemplate Default()
        {
            return new TileTemplate
            {
                // Initialize here
            };
        }
    }
}
