using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Data
{
    [Flags]
    public enum TileFlags
    {
        Walkable = 0,
        Solid = 1,
        Water = 2,
        Lava = 4,
        Spikes = 8,
        Pit = 16
    }

    public class Tile
    {
        public int TextureId { get; set; }
        public TileFlags TileFlags { get; set; }
        public bool Obstacle { get; set; }

        public Tile() { }

        public override string ToString()
        {
            return string.Format("TextureId: {0} \nType: {1}", TextureId, TileFlags.ToString());
        }
    }
}
