﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    // Map

    public enum MapLayerType
    {
        Tile,
        Entity
    }

    // Tiles

    [Flags]
    public enum MovementFlags
    {
        None = 0,
        Obstacle = 1,
        Water = 2,
        Pit = 4
    }

    [Flags]
    public enum SpecialFlags
    {
        None = 0,
        Stairs = 1,
        Event = 2
    }
}
