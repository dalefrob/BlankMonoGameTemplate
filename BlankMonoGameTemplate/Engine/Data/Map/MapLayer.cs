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
        public MapLayer()
        {

        }

        public MapLayer (int _width, int _height, LayerTemplate _template)
        {
            width = _width;
            height = _height;
            template = _template; 

            Name = _template.Name;
            TypeOfLayer = _template.TypeOfLayer;
            Initialize();         
        }

        public void Initialize()
        {
            // Load tileset
            Tileset = Tileset.GetTileset(TilesetName);
            // Load tiles from tileset
            Tiles = new Tile[width, height]; 
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var singleDimIndex = (y * width) + x;
                    Tiles[x, y] = Tileset.GetTile(template.TileTemplateIds[singleDimIndex]);
                }
            }
        }

        public readonly int width;
        public readonly int height;
        public readonly LayerTemplate template;

        public string Name { get; set; }
        public MapLayerType TypeOfLayer { get; set; }

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
