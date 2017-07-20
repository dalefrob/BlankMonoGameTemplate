using BlankMonoGameTemplate.Engine.Data.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate
{
    public class MapManager : IGameManager
    {
        public bool CollisionAt(int _x, int _y)
        {
            var result = false;
            if (Map.GetLayersOfType(LayerType.Collision).First().IDArray[_x, _y] != 0)
            {
                result = true;
            }
            return result;
        }

        public bool OutOfBounds(int _x, int _y)
        {
            return ((_x < 0 || _x >= Map.Width) || (_y < 0 || _y >= Map.Height));           
        }

        public Map Map { get; set; }
    }
}
