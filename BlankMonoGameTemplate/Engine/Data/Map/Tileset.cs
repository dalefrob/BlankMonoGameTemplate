﻿﻿using System;
using System.Linq;
using System.Collections.Generic;
using MonoGame.Extended.TextureAtlases;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using BlankMonoGameTemplate.Engine.Data;
using Newtonsoft.Json;

namespace BlankMonoGameTemplate.Engine
{
    /// <summary>
    /// Tileset.
    /// </summary>
    public class Tileset
    {
        #region Static
        static Dictionary<string, Tileset> loadedTilesets = new Dictionary<string, Tileset>();

        public static Tileset GetTileset(string _tilesetName)
        {
            Tileset result;
            if (!loadedTilesets.ContainsKey(_tilesetName))
            {
                // Load the tileset if its not in the dictionary
                result = Helper.LoadTileset(_tilesetName);
                loadedTilesets.Add(_tilesetName, result);
            }

            return loadedTilesets[_tilesetName];
        }
        #endregion

        /// <summary>
        /// Create a tileset with a template from scratch.
        /// </summary>
        /// <param name="_content"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Tileset (string _name, int _tilesize, string[] _filenames)
        {
            Name = _name;
            Tilesize = _tilesize;
            Filenames = _filenames;
            BuildAtlases();
            loadedTilesets.Add(Name, this);
        }

        public Tileset() { }

        public void BuildAtlases()
        {
            Dictionary<int, TileModel> tilemodels = new Dictionary<int, TileModel>();
            var blankTileData = new TileModel(0, null);
            tilemodels.Add(0, blankTileData);

            var globalIndex = 1;
            foreach (string filename in Filenames)
            {
                var texture = Content.Load<Texture2D>("Tiles/Objects/" + filename);
                var newAtlas = new TextureAtlas(filename, texture);
                var tilesHorizontal = newAtlas.Texture.Width / Tilesize;
                var tilesVertical = newAtlas.Texture.Height / Tilesize;
                
                for (int y = 0; y < tilesVertical; y++)
                {
                    for (int x = 0; x < tilesHorizontal; x++)
                    {
                        var i = (y * tilesHorizontal) + x;
                        newAtlas.CreateRegion(filename + i, x * Tilesize, y * Tilesize, Tilesize, Tilesize);
                        
                        var tileModel = new TileModel
                        {
                            ID = globalIndex,
                            AtlasName = filename,
                            RegionId = i,
                        };

                        tilemodels.Add(globalIndex, tileModel);
                        globalIndex++;
                    }
                }

                atlases.Add(filename, newAtlas);
            }

            if (tileModels.Count == 0)
            {
                tileModels = tilemodels;
            }
        }

        /// <summary>
        /// Gets a new tile. Public accessor to the dictionary.
        /// </summary>
        /// <returns>The tile.</returns>
        /// <param name="globalId">Global identifier.</param>
        public Tile GetTile(int globalId)
        {
            var tileTemplate = tileModels[globalId];
            var textureRegion = Tile.BlankRegion(16);
            if (tileTemplate.AtlasName != null)
            {
                textureRegion = atlases[tileTemplate.AtlasName].GetRegion(tileTemplate.RegionId);
            }

            var newTile = new Tile(textureRegion).LoadTemplate(tileTemplate);
            return newTile;
        }

        /// <summary>
        /// Atlases holding the textures of individual tiles
        /// </summary>
        private Dictionary<string, TextureAtlas> atlases = new Dictionary<string, TextureAtlas>();

        /// <summary>
        /// Map of global tile id to its data
        /// </summary>
        private Dictionary<int, TileModel> tileModels = new Dictionary<int, TileModel>();

        [JsonIgnore]
        public int TotalTiles
        {
            get
            {
                return tileModels.Count;
            }
        }

        [JsonIgnore]
        public ContentManager Content
        {
            get { return GameServices.GetService<ContentManager>(); }
        }

        #region JSONSerialization
        public string Name { get; set; }
        public int Tilesize { get; set; }
        public string[] Filenames { get; set; }
        public Dictionary<int, TileModel> TileTemplates
        {
            get { return tileModels; }
        }
        #endregion
    }
}
