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
    public class MapLayerTemplate
    {
        public enum LayerType
        {
            Tile,
            Entity
        }

        public MapLayerTemplate() 
        {
            TileIds = new List<int>();
        }

        public MapLayerTemplate(MapTemplate map, LayerType typeOfLayer) : this()
        {
            TypeOfLayer = typeOfLayer;
            for (int i = 0; i < map.Width * map.Height; i++)
            {
                TileIds.Add(0);
            }
        }

        public string LayerName { get; set; }
        public string TilesetName {	get; set; }

        public List<int> TileIds = new List<int>();
        public LayerType TypeOfLayer { get; set; }
    }
}
