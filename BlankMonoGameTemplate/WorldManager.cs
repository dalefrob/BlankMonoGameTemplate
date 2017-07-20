using BlankMonoGameTemplate.Engine;
using BlankMonoGameTemplate.Engine.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate
{
    /// <summary>
    /// Keeps track of everything happening in the world.
    /// </summary>
    public class WorldManager : IGameManager
    {
        public WorldManager()
        {

        }

        // The currently loaded map
        protected _Map _map;
        public _Map CurrentMap
        {
            get { return _map; }
        }

        public EntitySystem EntityManager { get; private set; }

        public Dictionary<Direction, Vector2> DirectionMap = new Dictionary<Direction, Vector2>
        {
            { Direction.Up, new Vector2(0, -1) },
            { Direction.Right, new Vector2(1, 0) },
            { Direction.Down, new Vector2(0, 1) },
            { Direction.Left, new Vector2(-1, 0) },
        };
    }
}
