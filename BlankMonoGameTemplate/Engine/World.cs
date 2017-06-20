using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;

namespace BlankMonoGameTemplate.Engine
{
    public class World : IUpdate
    {
        public World(Game game)
        {
            Game = game;

        }

		public void Update(GameTime gameTime)
		{
			
		}

        bool[,] _collisions;
        Tile[,] _tiles;

        public List<Player> Players = new List<Player>();

        Map _map;
		public Map Map
        {
            get { return _map; }
            set {
                _map = value;
                _tiles = new Tile[Map.Width, Map.Height];
                _collisions = new bool[Map.Width, Map.Height];
                for (int y = 0; y < Map.Height; y++)
                {
					for (int x = 0; x < Map.Width; x++)
					{
                        // Get a flattened tile
                        var _flatTile = new Tile();
                        foreach(var layer in Map.Layers)
                        {
                            _flatTile.TileFlags |= Map.Tilesets[layer.TilesetName].GetTile(x, y).TileFlags;
                        }
                        _tiles[x, y] = _flatTile;
                        if (_flatTile.TileFlags.HasFlag(TileFlags.Solid))
                        {
                            _collisions[x, y] = true;
                        }
					}
                }
            }
        }

        public Point GetWorldPointFromPosition(Vector2 position)
        {
            var tilePos = position / Map.TileSize;
            return tilePos.ToPoint();
        }

        public void MoveObjectToTile(IMovable movable, int x, int y)
        {
            if(!(x < 0 || x >= Map.Width || y < 0 || y >= Map.Height)) // If inside bounds of map
            {
				if (!_collisions[x, y]) // If no collisions
				{
					var newPos = new Vector2(Map.TileSize * x, Map.TileSize * y);
					movable.Position = newPos;
				}
            }
        }

		public void MoveObjectToTile(IMovable movable, Point point)
		{
			MoveObjectToTile(movable, point.X, point.Y);
		}

        public Game Game
        {
            get;
            private set;
        }
    }
}
