using MonoGame.Extended.TextureAtlases;
using Newtonsoft.Json;
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
    public class TileModel
    {
        public TileModel() { }

        public TileModel(int regionId, string atlasName)
            : this()
        {
            RegionId = regionId;
            AtlasName = atlasName;
        }

        [JsonIgnore]
        public int ID { get; set; }
        public int RegionId { get; set; }
        public string AtlasName { get; set; }

        public MovementFlags TileFlags { get; set; }
        public SpecialFlags SpecialFlags { get; set; }

        public static TileModel Default()
        {
            return new TileModel
            {
                RegionId = 0,
                AtlasName = null,
                TileFlags = MovementFlags.None,
                SpecialFlags = SpecialFlags.None
            };
        }
    }
}
