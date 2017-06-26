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

        public Game1 Game
        {
            get;
            private set;
        }

        EntityManager entityManager;
        MapRenderer mapRenderer;
    }
}
