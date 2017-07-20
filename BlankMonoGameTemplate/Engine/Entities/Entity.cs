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

        }
        
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Sprite);
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public EntitySystem Manager { get; set; }

        public string Name { get; set; }
        public bool isAlive { get; set; }
        public Sprite Sprite { get; set; }
        public Vector2 Position 
        { 
            get { return Sprite.Position; }
            set { Sprite.Position = value; }     
        }
        public Point MapLocation { get; set; }
    }
}
