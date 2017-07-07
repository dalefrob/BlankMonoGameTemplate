﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

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
            Tileset = Tileset.GetTileset("Default");
            Tiles = new Tile[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var singleDimIndex = (y * width) + x;
                    Tiles[x, y] = Tileset.GetTile(0);
                }
            }    
        }

        public void LoadTemplate(LayerTemplate _template)
        {
            Name = _template.Name;
            TypeOfLayer = _template.TypeOfLayer;
            Tileset = Tileset.GetTileset(TilesetName);
            Tiles = new Tile[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var singleDimIndex = (y * width) + x;
                    Tiles[x, y] = Tileset.GetTile(_template.TileTemplateIds[singleDimIndex]);
                }
            }
        }

        public readonly int width;
        public readonly int height;

        string _name = "Default";
        public string Name {
            get { return _name; }
            set { _name = value; }
        }
        MapLayerType _typeOfLayer = MapLayerType.Tile;
        public MapLayerType TypeOfLayer {
            get { return _typeOfLayer; }
            set { _typeOfLayer = value; }
        }

        public string TilesetName {
            get { return Tileset.Name; } 
        }
        public Tileset Tileset { get; set; }
        /// <summary>
        /// GAME READY TILES. Don't get confused!!
        /// </summary>
        public Tile[,] Tiles { get; set; }
        
        // Experimental Properties
        public Color Tint { get; set; }

    }
}
