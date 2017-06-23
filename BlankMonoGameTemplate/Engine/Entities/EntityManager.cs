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
        public EntityManager(Game1 game, World world)
        {
            Game = game;
            World = world;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            Entities.ForEach(e => e.Update(gameTime));
        }

        /// <summary>
        /// Draw all entities on a single spritebatch
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            Entities.ForEach(e => e.Draw(spriteBatch, gameTime));
            spriteBatch.End();
        }

        public T CreateEntity<T>() where T : Entity, new()
        {
            T _entity = new T()
            { 
                Manager = this 
            };

            AddEntity(_entity);
            return _entity;
        }

        public void AddEntity(Entity e)
        {
            if (!Entities.Contains(e))
            {
                e.LandedTile += Entity_LandedTile;
                Entities.Add(e);
            }
        }

        public void RemoveEntity(Entity e)
        {
            if (Entities.Contains(e))
            {
                e.LandedTile -= Entity_LandedTile;
                Entities.Remove(e);
            }
        }

        List<Entity> Entities = new List<Entity>();

        public Game1 Game
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

        #region Events

        void Entity_LandedTile(object sender, EntityEventArgs e)
        {
            Console.WriteLine(string.Format("{0} landed on tile: {1}", (sender as Entity).Name, e.MapCoord));
        }


        #endregion
    }
}
