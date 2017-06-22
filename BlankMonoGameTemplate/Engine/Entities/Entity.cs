using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Entities
{
    public class Entity : IUpdate
    {
        public Entity(World world)
        {
            World = world;         
            spriteBatch = new SpriteBatch(world.Game.GraphicsDevice);
        }
        
        public virtual void Step(Direction direction)
        {
            if (_moving) return; // TODO - change this

            var _mapCoord = World.GetMapCoordFromPosition(Position);
            int x = _mapCoord.X;
            int y = _mapCoord.Y;
            switch (direction)
            {
                case Direction.Up:
                    y--;
                    break;
                case Direction.Right:
                    x++;
                    break;
                case Direction.Down:
                    y++;
                    break;
                case Direction.Left:
                    x--;
                    break;
            }
            var _newCoord = new Point(x, y);
            DestinationPosition = World.GetPositionFromMapCoord(_newCoord);
        }

        public virtual void Update(GameTime gameTime)
        {
            // Smooth move to destination tile
            if (_moving)
            {              
                var direction = Vector2.Normalize(destinationPos - _originalPos);
                Position += direction * _moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Vector2.Distance(Position, destinationPos) >= _distance)
                {
                    Position = destinationPos;
                    _moving = false;
                }

                
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Sprite);
            spriteBatch.End();
        }

        public World World { get; private set; }
        public Sprite Sprite { get; set; }
        public Vector2 Position 
        { 
            get { return Sprite.Position; }
            set { Sprite.Position = value; }     
        }

        bool _moving = false;
        float _moveSpeed = 50f;
        Vector2 _originalPos = Vector2.Zero;
        float _distance = 0;
        Vector2 destinationPos;
        public Vector2 DestinationPosition
        {
            get
            {
                return destinationPos;
            }
            set
            {
                destinationPos = value;
                _originalPos = Position;
                _distance = Vector2.Distance(_originalPos, destinationPos);
                _moving = true;
            }
        }

        SpriteBatch spriteBatch;
    }
}
