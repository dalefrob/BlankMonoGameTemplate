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
                result = new Tileset(Helper.LoadTilesetData(_tilesetName));
                loadedTilesets.Add(_tilesetName, result);
            }

            return loadedTilesets[_tilesetName];
        }

        /// <summary>
        /// Create a tileset with a template from scratch.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Tileset CreateNew(ContentManager content, string name, int tileSize, string[] filenames)
        {
            var _tileset = new Tileset();
            var _tilesetData = new TilesetTemplate
            {
                Filenames = filenames.ToList(),
                Name = name,
                TileSize = tileSize
            };

            // Add blank tile
            var _blankTileData = new TileTemplate(0, null);
            _tilesetData.TileTemplates.Add(_blankTileData);

            var _gIndex = 1; // Global index starts at 1 due to blank tile being zero
            // Add tiles in all files
            foreach (string filename in _tilesetData.Filenames)
            {
                var _texture = content.Load<Texture2D>("Tiles/Objects/" + filename);
                var _newAtlas = new TextureAtlas(filename, _texture);
                var _tilesX = _texture.Width / _tilesetData.TileSize;
                var _tilesY = _texture.Height / _tilesetData.TileSize;
                for (int y = 0; y < _tilesY; y++)
                {
                    for (int x = 0; x < _tilesX; x++)
                    {
                        var i = (y * _tilesX) + x;
                        _newAtlas.CreateRegion(filename + i, x * _tilesetData.TileSize, y * _tilesetData.TileSize, _tilesetData.TileSize, _tilesetData.TileSize);

                        var _tileData = new TileTemplate
                        {
                            AtlasName = filename,
                            RegionId = i
                        };

                        _tilesetData.TileTemplates.Add(_tileData);
                        _gIndex++;
                    }
                }
                // Add a reference to the initialized atlas
                _tileset.atlases.Add(filename, _newAtlas);
            }

            // Save the data
            Helper.SaveTilesetData(_tilesetData);
            _tileset.Template = _tilesetData;

            return _tileset;
        }
        #endregion

        public Tileset() { }

        public Tileset(TilesetTemplate _template)
        {
            LoadTemplate(_template);
        }

        /// <summary>
        /// Name of the tileset, not the texture atlas.
        /// </summary>
        public string Name
        {
            get
            {
                return Template.Name;
            }
        }
        /// <summary>
        /// XML loaded Tileset Data
        /// </summary>
        public TilesetTemplate Template { get; private set; }

        /// <summary>
        /// Load the template data into this tileset. Doesn't assign tile textures!
        /// </summary>
        /// <param name="_template"></param>
        public void LoadTemplate(TilesetTemplate _template)
        {
            tileTemplates.Clear(); 

            var globalIndex = 0;
            foreach (var filename in _template.Filenames)
            {
                // Load the tileset texture
                var contentMgr = GameServices.GetService<ContentManager>();
                var _texture = contentMgr.Load<Texture2D>("Tiles/Objects/" + filename);
                var _newAtlas = new TextureAtlas(filename, _texture);
                atlases.Add(filename, _newAtlas);

                for (int i = 0; i < _template.TileTemplates.Count; i++)
                {
                    this.tileTemplates.Add(globalIndex, _template.TileTemplates[i]);
                    globalIndex++;
                }              
            }

            Template = _template;
        }
        
        /// <summary>
        /// Gets a new tile. Public accessor to the dictionary.
        /// </summary>
        /// <returns>The tile.</returns>
        /// <param name="globalId">Global identifier.</param>
        public Tile GetTile(int globalId)
        {
            var tileTemplate = Template.TileTemplates[globalId];
            var textureRegion = atlases[tileTemplate.AtlasName].GetRegion(tileTemplate.RegionId);

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
        private Dictionary<int, TileTemplate> tileTemplates = new Dictionary<int, TileTemplate>();

        public int TotalTiles
        {
            get
            {
                return tileTemplates.Count;
            }
        }
    }
}
