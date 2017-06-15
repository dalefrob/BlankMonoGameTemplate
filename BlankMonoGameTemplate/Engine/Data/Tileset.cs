﻿using System;
using System.Collections.Generic;
using MonoGame.Extended.TextureAtlases;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework;

namespace BlankMonoGameTemplate.Engine
{
    [Flags]
    public enum TileFlags 
    {
        Walkable = 0,
        Solid = 1,
        Water = 2,
        Lava = 4,
        Spikes = 8,
        Pit = 16
    }

    public class Tileset
    {
        public Tileset() { }

        public Tileset(TextureAtlas textureAtlas, int tileSize) {
			TileSize = tileSize;
            TextureSheetName = textureAtlas.Name;
			TileSheet = textureAtlas;
            GenerateTiles();
        }

        public Tileset(Texture2D texture, int tileSize, string textureSheetName)
        {
            TileSize = tileSize;
            TextureSheetName = textureSheetName;
            TileSheet = TextureAtlas.Create(TextureSheetName, texture, tileSize, tileSize);
            GenerateTiles();
        }

        void GenerateTiles()
        {
            Tiles = new List<Tile>();
            for (int i = 0; i < TileSheet.RegionCount; i++)
            {
                Tiles.Add(new Tile()
                {
                    TextureId = i,
                    Type = TileFlags.Walkable
                });
            }
        }

        public TextureRegion2D GetTileTexture(int tileId)
        {
            return TileSheet.GetRegion(tileId);
        }

        /// <summary>
        /// Gets the tile by 1d array index
        /// </summary>
        /// <returns>The tile.</returns>
        /// <param name="index">Index.</param>
        public Tile GetNewTile(int index) {
            int x = index % TilesHorizontal;
            int y = (index / TilesHorizontal);
            var tile = new Tile
            {
                TextureId = Index2dTo1d(TilesHorizontal, x, y),
                Type = TileFlags.Walkable
            };
            return tile;
        }

        public string TextureSheetName
        {
            get;
            set;
        }

        public int TileSize
        {
            get;
            set;
        }

        public List<Tile> Tiles
        {
            get;
            set;
        }

        [XmlIgnore]
        public TextureAtlas TileSheet
        {
            get;
            set;
        }

        int TilesHorizontal {
            get {
                return TileSheet.Texture.Width / TileSize;
            }
        }

        int TilesVertical {
            get {
                return TileSheet.Texture.Height / TileSize;
            }
        }

        int Index2dTo1d(int xMax, int x, int y) {
            return (y * xMax) + x;
        }

        #region Static
        public static void SaveToFile(Tileset tileset, string filename)
        {
            XmlSerializer x = new XmlSerializer(tileset.GetType());
            StreamWriter writer = new StreamWriter(filename);
            x.Serialize(writer, tileset);
        }

        public static Tileset LoadFromFile(Game game, string filename)
        {
            Tileset result;
            XmlSerializer x = new XmlSerializer(typeof(Tileset));
            StreamReader reader = new StreamReader(filename);
            result = (Tileset)x.Deserialize(reader);
            var texture = game.Content.Load<Texture2D>("Tiles/Objects/" + result.TextureSheetName);
            var sheet = TextureAtlas.Create(result.TextureSheetName, texture, result.TileSize, result.TileSize);
            result.TileSheet = sheet;
            
            return result;
        }
        #endregion
    }

    public struct Tile
    {
        public int TextureId { get; set; }
        public TileFlags Type { get; set; }

        public override string ToString()
        {
            return string.Format("TextureId: {0} \nType: {1}", TextureId, Type.ToString());
        }
    }
}
