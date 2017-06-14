using System;
using System.Collections.Generic;
using MonoGame.Extended.TextureAtlases;

namespace BlankMonoGameTemplate.Engine
{
	[Flags]
	public enum TileCollisions
	{
		None = 0,
		TopLeft = 1,
		TopRight = 2,
        BottomRight = 4,
        BottomLeft = 8
	}

    public enum TileType 
    {
        Walkable,
        Solid,
        Water
    }

    public class Tileset
    {
        public Tileset(TextureAtlas textureAtlas, int tileSize) {
			_tileSize = tileSize;
			TileSheet = textureAtlas;
            CollisionLayer = new int[TilesHorizontal, TilesVertical];
            ClearLayers();
        }

        public Tileset(TextureAtlas textureAtlas, int tileSize, int[,] collisionLayer) : this(textureAtlas, tileSize)
        {           
            CollisionLayer = collisionLayer;
        }

        /// <summary>
        /// Gets the tile by 1d array index
        /// </summary>
        /// <returns>The tile.</returns>
        /// <param name="index">Index.</param>
        public Tile GetTile(int index) {
            int x = index % TilesHorizontal;
            int y = (index / TilesHorizontal);
            var tile = new Tile
            {
                Texture = TileSheet.GetRegion(Index2dTo1d(TilesHorizontal, x, y)),
                TextureId = Index2dTo1d(TilesHorizontal, x, y),
                Collisions = (TileCollisions)CollisionLayer[x, y]
            };
            return tile;
        }

        void ClearLayers() {
            for (int y = 0; y < TilesVertical; y++) {
                for (int x = 0; x < TilesHorizontal; x++) {                   
                    CollisionLayer[x, y] = 0;
                }
            }
        }

        public TextureAtlas TileSheet
        {
            get;
            set;
        }

        public int[,] CollisionLayer {
            get;
            set;
        }

        readonly int _tileSize;

        int TilesHorizontal {
            get {
                return TileSheet.Texture.Width / _tileSize;
            }
        }

        int TilesVertical {
            get {
                return TileSheet.Texture.Height / _tileSize;
            }
        }

        int Index2dTo1d(int xMax, int x, int y) {
            return (y * xMax) + x;
        }
    }

    public struct Tile
    {
        public TextureRegion2D Texture { get; set; }
        public int TextureId { get; set; }
        public TileCollisions Collisions { get; set; }
    }
}
