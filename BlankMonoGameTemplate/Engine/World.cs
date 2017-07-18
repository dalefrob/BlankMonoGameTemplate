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
using BlankMonoGameTemplate.Engine.Data;
using MonoGame.Extended.ViewportAdapters;

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
        public World(Game game, Map map)
        {
            Game = game;
            Map = map;

        }

        public void Initialize()
        {
            InitMap();
            InitEntities();
        }

        private void InitEntities()
        {
            /* CREATE ENTITIES FOR TESTING */
            var player = (Player)entityManager.CreateEntity<Player>().SetMapPosition(0, 0);
            player.Name = "Player";
            player.LocalPlayer = true;

            var key = (Key)entityManager.CreateEntity<Key>().SetMapPosition(7, 0);

            var door = (Door)entityManager.CreateEntity<Door>().SetMapPosition(3, 4);
            door.RequiresKey = true;

            entityManager.CreateEntity<Monster>().SetMapPosition(9, 9);
        }

        private void InitMap()
        {
            /* INIT MAP */
            Collisions = new bool[Map.Width, Map.Height];
            AStarNodes = new AStarNode[Map.Width, Map.Height];
            for (int i = 0; i < Map.Width * Map.Height; i++)
            {
                var x = i % Map.Width;
                var y = i / Map.Width;
                var currentNode = new AStarNode();
                foreach (var layer in Map.Layers)
                {
                    var index = (y * Map.Width) + x;
                    var tile = layer.Value.Tiles[x, y];
                    if (tile.Obstacle)
                    {
                        currentNode.Obstacle = true;

                        AStarNodes[x, y] = currentNode;
                        Collisions[x, y] = true;
                    }
                }
            }
        }

        public Map Map { get; set; }

        public List<Entity> GetEntitiesAtCoord(Point coord)
        {
            var result = new List<Entity>();
            result = entityManager.GetEntitiesOfType<Entity>().Where(e => e.MapCoordinate == coord).ToList();
            return result;
        }

        public Point GetMapCoordFromPosition(Vector2 position)
        {
            var offsetPos = position - mapRenderer.Position;          
            return (offsetPos / Map.Tilesize).ToPoint();
        }

        public Vector2 GetPositionFromMapCoord(Point coord)
        {
            return mapRenderer.Position + new Vector2(coord.X * Map.Tilesize, coord.Y * Map.Tilesize);
        }

        public void MoveObjectToTile(Entity entity, int x, int y)
        {
            entity.Position = GetPositionFromMapCoord(new Point(x, y));
        }

		public void MoveObjectToTile(Entity entity, Point point)
		{
			MoveObjectToTile(entity, point.X, point.Y);
		}

        public List<Point> GetTileRangeCircle(Point origin, float tileRange)
        {
            var points = new List<Point>();
            var worldPos = GetPositionFromMapCoord(origin) + new Vector2(Map.Tilesize / 2); // Map.TileSize / 2 will get the center position of the tile
            // Every 20 degrees, query tiles at radius distance
            for (int a = 0; a < 360; a = a + 20)
            {
                for (int r = 1; r <= tileRange; r++)
                {
                    float x = (float)((r * Map.Tilesize) * Math.Cos(a * Math.PI / 180F)) + worldPos.X;
                    float y = (float)((r * Map.Tilesize) * Math.Sin(a * Math.PI / 180F)) + worldPos.Y;
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

        public Game Game
        {
            get;
            private set;
        }

        public bool[,] Collisions
        {
            get;
            private set;
        }

        public AStarNode[,] AStarNodes
        {
            get;
            set;
        }

        EntityManager entityManager;
        MapRenderer mapRenderer;
        public Camera2D Camera { get; private set; }
    }
}
