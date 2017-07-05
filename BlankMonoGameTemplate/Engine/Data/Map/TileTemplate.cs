using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine
{
    /// <summary>
    /// Tile specific data for import and export
    /// </summary>
    public class TileTemplate
    {
        // Identifying properties
        //public int GlobalID { get; set; }
        public int RegionId { get; set; }
        public string AtlasName { get; set; }

        // Game specific properties
        public MovementFlags TileFlags { get; set; }
        public SpecialFlags SpecialFlags { get; set; }

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
                RegionId = 0,
                AtlasName = null,
                TileFlags = MovementFlags.None,
                SpecialFlags = SpecialFlags.None
            };
        }
    }
}
