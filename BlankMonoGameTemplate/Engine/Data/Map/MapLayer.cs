﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BlankMonoGameTemplate.Engine
{
    /// <summary>
    /// Map layer. Not Serializable.
    /// MapTemplate holds layer information.
    /// </summary>
    public class MapLayer
    {
        public MapLayer (int _width, int _height)
        {
            width = _width;
            height = _height;
            TileIds = new int[_width, _height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    TileIds[x, y] = 0;
                }
            }             
        }

        public void Build(Map _map)
        {
            Tileset = Tileset.GetTileset("Default");
            Tiles = new Tile[_map.Width, _map.Height];
            for (int y = 0; y < _map.Height; y++)
            {
                for (int x = 0; x < _map.Width; x++)
                {
                    Tiles[x, y] = Tileset.GetTile(TileIds[x, y]);
                }
            }
        }

        [JsonIgnore]
        public readonly int width;
        [JsonIgnore]
        public readonly int height;

        string _name = "Default";
        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public MapLayerType TypeOfLayer = MapLayerType.Tile;

        [JsonIgnore]
        public string TilesetName {
            get { return Tileset.Name; } 
        }

        [JsonIgnore]
        public Color Tint { get; set; }
        [JsonIgnore]
        public Tileset Tileset { get; set; }
        [JsonIgnore]
        public Tile[,] Tiles { get; set; }
        public int[,] TileIds { get; set; }
    }
}
