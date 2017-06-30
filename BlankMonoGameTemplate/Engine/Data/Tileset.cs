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
        #region Static
        public static Dictionary<string, Tileset> Loaded = new Dictionary<string, Tileset>();

        /// <summary>
        /// Create a tileset from scratch as well as generate Data
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
            _tilesetData.TileData.Add(_blankTileData);

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

                        _tilesetData.TileData.Add(_tileData);
                        _gIndex++;
                    }
                }
                // Add a reference to the initialized atlas
                _tileset.atlases.Add(filename, _newAtlas);
            }

            // Save the data
            Helper.SaveTilesetData(_tilesetData);
            _tileset.Data = _tilesetData;

            return _tileset;
        }
        #endregion

        public Tileset() { }

        public Tileset(TilesetTemplate data)
        {
            SetData(data);
        }

        /// <summary>
        /// Name of the tileset, not the texture atlas.
        /// </summary>
        public string Name
        {
            get
            {
                return Data.Name;
            }
        }
        /// <summary>
        /// XML loaded Tileset Data
        /// </summary>
        public TilesetTemplate Data
        {
            get;
            private set;
        }

        public void SetData(TilesetTemplate _data)
        {
            Data = _data;

            var _gIndex = 0;
            foreach (var filename in Data.Filenames)
            {
                var contentMgr = GameServices.GetService<ContentManager>();
                var _texture = contentMgr.Load<Texture2D>("Tiles/Objects/" + filename);
                var _newAtlas = new TextureAtlas(filename, _texture);
                atlases.Add(filename, _newAtlas);

                var _tilesX = _texture.Width / _data.TileSize;
                var _tilesY = _texture.Height / _data.TileSize;
                for (int y = 0; y < _tilesY; y++)
                {
                    for (int x = 0; x < _tilesX; x++)
                    {
                        var i = (y * _tilesX) + x;
                        _newAtlas.CreateRegion(filename + i, x * _data.TileSize, y * _data.TileSize, _data.TileSize, _data.TileSize);
                        var _tileData = new TileTemplate
                        {
                            AtlasName = filename,
                            RegionId = i
                        };
                        _data.TileData.Add(_tileData);
                        _gIndex++;
                    }
                }
            }

            allTileData = new Dictionary<int, Tile>();
            for (int i = 0; i < Data.TileData.Count; i++)
            {
                var tile = new Tile
                {
                    TileData = Data.TileData[i],
                    Texture = atlases[
                };
                allTileData.Add(i, tile);
            }
        }

        public Tile GetTile(int globalId)
        {
            var _newTile = new Tile();
            _newTile.TileData = allTileData[globalId];
            if (_newTile.TileData.AtlasName == null)
            {
                var _blankTexture = new Texture2D(GameServices.GetService<GraphicsDevice>(), Data.TileSize, Data.TileSize);
                var _blankRegion = new TextureRegion2D(_blankTexture);
                _newTile.Texture = _blankRegion;
            }
            else
            {
                _newTile.Texture = atlases[_newTile.TileData.AtlasName].GetRegion(_newTile.TileData.RegionId);
            }
            return _newTile;
        }

        /// <summary>
        /// Atlases holding the textures of individual tiles
        /// </summary>
        private Dictionary<string, TextureAtlas> atlases = new Dictionary<string, TextureAtlas>();

        /// <summary>
        /// Map of global tile id to its data
        /// </summary>
        private Dictionary<int, Tile> allTileData = new Dictionary<int, Tile>();

        public int TotalTiles
        {
            get
            {
                return allTileData.Count;
            }
        }
    }
}
