using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace BlankMonoGameTemplate.Engine
{
    /// <summary>
    /// Map layer.
    /// 2D array of int with associated texture atlas.
    /// </summary>
    public class MapLayer
    {
        public MapLayer(GameMap gameMap, Tileset tileset) {
            _gameMap = gameMap;
            Tileset = tileset;

            TileIds = new int[_gameMap.Width, _gameMap.Height];                  
        }

        public void FloodWithTileId(int id) {
			for (int y = 0; y < _gameMap.Height; y++)
			{
				for (int x = 0; x < _gameMap.Width; x++)
				{
					TileIds[x, y] = id;
				}
			}
		}

        public int[,] TileIds
        {
            get;
            set;
        }

        public Tileset Tileset {
            get;
            private set;
        }

        internal Tile GetTile(int x, int y)
        {
            var id = TileIds[x, y];
            return Tileset.GetTile(id);
        }

        readonly GameMap _gameMap;
    }
}
