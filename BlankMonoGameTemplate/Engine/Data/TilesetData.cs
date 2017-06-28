﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Data
{
    public class TilesetData
    {
        public TilesetData() 
        {
            TileData = new List<Tile>();
        }

        /// <summary>
        /// Name of this tileset
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// PNG etc. file
        /// </summary>
        public string ImageFilename { get; set; }
        /// <summary>
        /// Properties that correspond to tiles
        /// </summary>
        public List<Tile> TileData { get; set; }
        /// <summary>
        /// Size of the individual tiles
        /// </summary>
        public int TileSize { get; set; }
    }
}