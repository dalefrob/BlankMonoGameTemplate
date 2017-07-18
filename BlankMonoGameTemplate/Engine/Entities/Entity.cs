using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Entities
{
    public class Entity : IUpdate
    {
        public Entity() 
        {
            Sprite = new Sprite(WorldScreen.Textures2D["GUI0"].GetRegion(0));
        }
        
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Sprite);
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        #region BuilderMethods

        public Entity SetMapPosition(Point p)
        {
            World.MoveObjectToTile(this, p);
            return this;
        }

        public Entity SetMapPosition(int x, int y)
        {
            World.MoveObjectToTile(this, new Point(x, y));
            return this;
        }

        #endregion

        public EntityManager Manager { get; set; }
        public string Name { get; set; }
        public bool isAlive { get; set; }
        public Point MapCoordinate
        {
            get
            {
                return World.GetMapCoordFromPosition(Position);
            }
        }
        public Sprite Sprite { get; set; }
        public Vector2 Position 
        { 
            get { return Sprite.Position; }
            set { Sprite.Position = value; }     
        }    
    }

    public class EntityEventArgs : EventArgs
    {
        public Point MapCoord { get; set; }
        public EntityEventArgs()
        {
             
        }
    }
}
