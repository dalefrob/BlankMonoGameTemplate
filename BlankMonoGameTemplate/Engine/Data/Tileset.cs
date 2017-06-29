﻿using System;
using System.Linq;
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
    /// <summary>
    /// Tileset.
    /// </summary>
    public class Tileset
    {
        public static Tileset Create(ContentManager content, TilesetData data)
        {
            var _tileset = new Tileset();
            foreach (string filename in data.Filenames.Values)
            {
                var _newAltas = TextureAtlas.Create(filename, content.Load<Texture2D>("Tiles/Objects/" + filename), data.TileSize, data.TileSize);
                if(!_tileset.Atlases.ContainsKey(filename))
                {
                    _tileset.Atlases.Add(filename, _newAltas);
                    for (int r = 0; r < _newAltas.RegionCount; r++)
                    {
                        var newTile = new Tile(r, filename);
                        _tileset.AllTiles.Add();
                    }
                }

            }
            return _tileset;
        }

        public Tileset() { }

        public Tileset(ContentManager content, TilesetData data) 
        {
            Data = data;
            TextureAtlas = TextureAtlas.Create(data.Name, content.Load<Texture2D>("Tiles/Objects/" + data.ImageFilename), data.TileSize, data.TileSize);
            if (Data.Tiles.Count == 0)
            {
                for (int i = 0; i < TextureAtlas.RegionCount; i++)
                {
                    Data.Tiles.Add(new Tile() { TextureId = i });
                }
            }
        }


        public List<Tile> AllTiles = new List<Tile>();
        /// <summary>
        /// XML loaded Tileset Data
        /// </summary>
        public TilesetData Data { get; private set; }
        /// <summary>
        /// Atlases holding the textures of individual tiles
        /// </summary>
        public Dictionary<string, TextureAtlas> Atlases = new Dictionary<string, TextureAtlas>();

        public Tile GetTileData(string tileset, int x, int y)
        {
            return Data.Tiles[(y * TilesHorizontal) + x];
        }

        public Tile GetTileData(int index)
        {
            return Data.Tiles[index];
        }

        public TextureRegion2D TileTextureByIndex(int index)
        {
            return TextureAtlas.GetRegion(index);
        }

        public TextureRegion2D TileTextureByCoord(int x, int y)
        {
            return TileTextureByIndex((y * TilesHorizontal) + x);
        }

        public Dictionary<TileSetKey, Tile> Tiles = new Dictionary<TileSetKey, Tile>(); 

        public struct TileSetKey
        {
            public string TilesetName { get; set; }
            public int TextureRegionId { get; set; }
        }
    }
}
