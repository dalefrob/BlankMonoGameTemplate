using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;

namespace BlankMonoGameTemplate.Engine
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public class World : IUpdate
    {
        public World(Game game)
        {
            Game = game;

        }

		public void Update(GameTime gameTime)
		{
			
		}

        public List<Player> Players = new List<Player>();

        public Map Map { get; set; }

        public Point GetMapCoordFromPosition(Vector2 position)
        {
            var tilePos = position / Map.TileSize;
            return tilePos.ToPoint();
        }

        public Vector2 GetPositionFromMapCoord(Point coord)
        {
            return new Vector2(coord.X * Map.TileSize, coord.Y * Map.TileSize);
        }

        public void MoveObjectToTile(IMovable movable, int x, int y)
        {
            if(!(x < 0 || x >= Map.Width || y < 0 || y >= Map.Height)) // If inside bounds of map
            {
				if (!Map.Collisions[x, y]) // If no collisions
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
