using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Entities
{
    public class EntityManager : IUpdate
    {
        public EntityManager(Game game, World world)
        {
            Game = game;
            World = world;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            GameServices.AddService<EntityManager>(this);
        }

        public void Update(GameTime gameTime)
        {
            entities.ForEach(e => e.Update(gameTime));

            var deadEntities = entities.Where(e => !e.isAlive).ToArray();
            foreach (var e in deadEntities)
            {
                entities.Remove(e);
            }
        }

        /// <summary>
        /// Draw all entities on a single spritebatch
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            var transformMatrix = World.Camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: transformMatrix);
            entities.Where(e => e.isAlive).ToList().ForEach(e => e.Draw(spriteBatch, gameTime));
            spriteBatch.End();
        }

        public T CreateEntity<T>() where T : Entity, new()
        {
            T _entity = new T()
            { 
                Manager = this,
                isAlive = true
            };

            AddEntity(_entity);
            return _entity;
        }

        public void AddEntity(Entity e)
        {
            if (!entities.Contains(e))
            {
                if (e is Character)
                {
                    var c = (Character)e;
                    c.LandedTile += Entity_LandedTile;
                }
                entities.Add(e);
            }
        }

        public void RemoveEntity(Entity e)
        {
            if (entities.Contains(e))
            {
                if (e is Character)
                {
                    var c = (Character)e;
                    c.LandedTile -= Entity_LandedTile;
                }
                if (e.isAlive) e.isAlive = false;
            }
        }

        public List<Entity> GetEntitiesOfType<T>() where T : Entity
        {
            var result = entities.Where(e => e is T && e.isAlive).ToList();
            return result;
        }

        internal List<Entity> entities = new List<Entity>();

        public Game Game
        {
            get;
            private set;
        }

        public World World
        {
            get;
            set;
        }

        SpriteBatch spriteBatch;
        public Random random = new Random();

        #region Events

        void Entity_LandedTile(object sender, EntityEventArgs e)
        {
            Console.WriteLine(string.Format("{0} landed on tile: {1}, P: {2}, WP: {2}", (sender as Entity).Name, e.MapCoord, (sender as Entity).Position, (sender as Entity).Sprite.WorldPosition));
        }


        #endregion

        ~EntityManager()
        {
            entities.Clear();
            GameServices.RemoveService<EntityManager>();
        }
    }
}
