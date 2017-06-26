using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;
using BlankMonoGameTemplate.Engine.Entities;
using MonoGame.Extended.Sprites;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace BlankMonoGameTemplate.Engine
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    /// <summary>
    /// Controls all the objects and happenings that occur in the "Game World"
    /// </summary>
    public class World
    {
        public World(Game1 game, Map map)
        {
            Game = game;
            entityManager = new EntityManager(game, this);
            mapRenderer = new MapRenderer(game, map);
        }

        public void Initialize()
        {
            var player = entityManager.CreateEntity<Player>();
            player.Name = "Player";
            player.LocalPlayer = true;

            var key = (Key)entityManager.CreateEntity<Key>().SetMapPosition(7, 0);

            var door = (Door)entityManager.CreateEntity<Door>().SetMapPosition(3, 4);
            door.RequiresKey = true;

            entityManager.CreateEntity<Monster>().SetMapPosition(9, 9);

            /*
            var test = GetTileRangeCircle(new Point(6, 6), 1);
            //var test = GetTileRangeDiamond(new Point(6, 6), 4);
            foreach (var coord in test)
            {
                entityManager.CreateEntity<Monster>().SetMapPosition(coord);
            } 
            */
        }

		public void UpdateWorld(GameTime gameTime)
		{
            // Update map

            // Update entities
            entityManager.Update(gameTime);
		}

        public void DrawWorld(GameTime gameTime)
        {
            // Draw map
            mapRenderer.Draw(gameTime);
            // Draw entities
            entityManager.Draw(gameTime);
        }

        public Map Map
        {
            get { return mapRenderer.Map; }
        }

        public List<Entity> GetEntitiesAtCoord(Point coord)
        {
            var result = new List<Entity>();
            result = entityManager.ActiveEntities.Where(e => e.MapCoordinate == coord).ToList();
            return result;
        }

        public Point GetMapCoordFromPosition(Vector2 position)
        {
            var tilePos = position / Map.TileSize;
            return tilePos.ToPoint();
        }

        public Vector2 GetPositionFromMapCoord(Point coord)
        {
            return new Vector2(coord.X * Map.TileSize, coord.Y * Map.TileSize);
        }

        public void MoveObjectToTile(Entity entity, int x, int y)
        {
            var newPos = new Vector2(Map.TileSize * x, Map.TileSize * y);
            entity.Position = newPos;
        }

		public void MoveObjectToTile(Entity entity, Point point)
		{
			MoveObjectToTile(entity, point.X, point.Y);
		}

        public List<Point> GetTileRangeCircle(Point origin, float tileRange)
        {
            var points = new List<Point>();
            var worldPos = GetPositionFromMapCoord(origin) + new Vector2(Map.TileSize / 2); // Map.TileSize / 2 will get the center position of the tile
            // Every 20 degrees, query tiles at radius distance
            for (int a = 0; a < 360; a = a + 20)
            {
                for (int r = 1; r <= tileRange; r++)
                {
                    float x = (float)((r * Map.TileSize) * Math.Cos(a * Math.PI / 180F)) + worldPos.X;
                    float y = (float)((r * Map.TileSize) * Math.Sin(a * Math.PI / 180F)) + worldPos.Y;
                    var tileCoord = GetMapCoordFromPosition(new Vector2(x, y));
                    if (!points.Contains(tileCoord))
                    {
                        points.Add(tileCoord);
                    }
                }
            }

            return points;
        }

        /// <summary>
        /// Gets surrounding tiles in range in a diamond shape.
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="tileRange"></param>
        /// <returns></returns>
        public List<Point> GetTileRangeDiamond(Point origin, int tileRange)
        {
            var points = new List<Point>();
            for (int i = 1; i <= tileRange; i++)
            {
                points.Add(new Point(origin.X, origin.Y + i));
                points.Add(new Point(origin.X, origin.Y - i));
                points.Add(new Point(origin.X + i, origin.Y));
                points.Add(new Point(origin.X - i, origin.Y));
                if (i > 1)
                {
                    var o = i - 1;
                    points.Add(new Point(origin.X + o, origin.Y + o));
                    points.Add(new Point(origin.X - o, origin.Y + o));
                    points.Add(new Point(origin.X - o, origin.Y - o));
                    points.Add(new Point(origin.X + o, origin.Y - o));
                }
            }
            return points;
        }

        public Game1 Game
        {
            get;
            private set;
        }

        EntityManager entityManager;
        MapRenderer mapRenderer;
    }
}
