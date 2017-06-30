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
        Default = 0,
        Solid = 1,
        Water = 2,
        Lava = 4,
        Spikes = 8,
        Pit = 16
    }
}
