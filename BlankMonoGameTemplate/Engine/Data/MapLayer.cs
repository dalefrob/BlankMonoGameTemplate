﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using System.Xml.Serialization;

namespace BlankMonoGameTemplate.Engine
{
    /// <summary>
    /// Map layer.
    /// 2D array of int with associated texture atlas.
    /// </summary>
    public class MapLayer
    {
        public enum LayerType
        {
            Tile,
            Entity
        }

        public MapLayer() { }

        public MapLayer(MapData map, LayerType typeOfLayer)
        {
            TypeOfLayer = typeOfLayer;
            for (int i = 0; i < map.Width * map.Height; i++)
            {
                Tiles.Add(0);
            }
        }

        public string TilesetName
		{
			get;
			set;
		}

        public List<int> Tiles = new List<int>();
        public LayerType TypeOfLayer { get; set; }
    }
}
