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
                if (e is Character)
                {
                    var c = (Character)e;
                    c.LandedTile += Entity_LandedTile;
                }
                Entities.Add(e);
            }
        }

        public void RemoveEntity(Entity e)
        {
            if (Entities.Contains(e))
            {
                if (e is Character)
                {
                    var c = (Character)e;
                    c.LandedTile -= Entity_LandedTile;
                }
            }
        }

        List<Entity> Entities = new List<Entity>();

        public List<Entity> ActiveEntities
        {
            get
            {
                return Entities.Where(e => e.ToCleanup == false).ToList();
            }
        }

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
            Console.WriteLine(string.Format("{0} landed on tile: {1}", (sender as Entity).Name, e.MapCoord));
        }


        #endregion

        ~EntityManager()
        {
            GameServices.RemoveService<EntityManager>();
        }
    }
}
