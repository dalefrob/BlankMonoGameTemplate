using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Data
{
    public class AStarNode
    {
        public bool Obstacle { get; set; }
        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost
        {
            get
            {
                return GCost + HCost;
            }
        }
    }
}
