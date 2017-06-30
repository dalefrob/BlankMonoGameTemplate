using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Data
{
    /// <summary>
    /// Tile as represented on a map or in a tileset viewer
    /// </summary>
    public class Tile
    {
        public TextureRegion2D Texture { get; set; }
        public TileTemplate TileData { get; set; }

        public Tile()
        {

        }
    }
}
