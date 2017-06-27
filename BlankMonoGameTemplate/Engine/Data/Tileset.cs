﻿using System;
using System.Collections.Generic;
using MonoGame.Extended.TextureAtlases;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using BlankMonoGameTemplate.Engine.Data;

namespace BlankMonoGameTemplate.Engine
{
    public class Tileset
    {
        public Tileset(ContentManager content, string name, TilesetData data) 
        {
            Data = data;
            TextureAtlas = TextureAtlas.Create(data.Name, content.Load<Texture2D>("Tiles/Objects/" + data.ImageFilename), data.TileSize, data.TileSize);
            if (Data.TileData.Count == 0)
            {
                for (int i = 0; i < TextureAtlas.RegionCount; i++)
                {
                    Data.TileData.Add(new Tile() { TextureId = i });
                }
            }
        }

        /// <summary>
        /// XML loaded Tileset Data
        /// </summary>
        public TilesetData Data { get; private set; }
        /// <summary>
        /// Atlas holding the textures of individual tiles
        /// </summary>
        public TextureAtlas TextureAtlas { get; private set; }

        public int TilesVertical
        {
            get
            {
                return TextureAtlas.Texture.Height / Data.TileSize;
            }
        }

        public int TilesHorizontal
        {
            get
            {
                return TextureAtlas.Texture.Width / Data.TileSize;
            }
        }

        public Tile GetTileData(int x, int y)
        {
            return Data.TileData[(y * TilesHorizontal) + x];
        }

        public Tile GetTileData(int index)
        {
            return Data.TileData[index];
        }

        public TextureRegion2D TileTextureByIndex(int index)
        {
            return TextureAtlas.GetRegion(index);
        }

        public TextureRegion2D TileTextureByCoord(int x, int y)
        {
            return TileTextureByIndex((y * TilesHorizontal) + x);
        }     
    }
}
