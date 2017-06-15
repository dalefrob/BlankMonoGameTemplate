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
        public MapLayer()
        {
            Tiles = new List<int>();
        }

        public MapLayer(int width, int height) : this()
        {
            for (int i = 0; i < (width * height); i++)
            {
                Tiles.Add(0);
            }
        }

        public void FloodWithTileId(int id) {
            Tiles.Clear();
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tiles.Add(id);
            }
		}

		public string TilesetName
		{
			get;
			set;
		}

        public List<int> Tiles
        {
            get;
            set;
        }
    }
}
