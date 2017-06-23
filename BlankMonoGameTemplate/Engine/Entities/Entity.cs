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
        public EntityManager Manager { get; set; }

        public Entity() { }
        
        public virtual void Move(Direction direction, int tileCount = 1)
        {
            if (_moving) return; // TODO - change this

            var _mapCoord = World.GetMapCoordFromPosition(Position);
            int x = _mapCoord.X;
            int y = _mapCoord.Y;
            switch (direction)
            {
                case Direction.Up:
                    y -= tileCount;
                    break;
                case Direction.Right:
                    x += tileCount;
                    break;
                case Direction.Down:
                    y += tileCount;
                    break;
                case Direction.Left:
                    x -= tileCount;
                    break;
            }           
            var _newCoord = new Point(x, y);
            // Check if it is a legal move
            if (!(x < 0 || x >= World.Map.Width || y < 0 || y >= World.Map.Height))
            {
                if (!World.Map.Collisions[x, y])
                {
                    DestinationPosition = World.GetPositionFromMapCoord(_newCoord);
                }  
            }   
        }

        public virtual void Update(GameTime gameTime)
        {
            // Smooth move to destination tile
            if (_moving)
            {
                _lerpVal += _timeToMove / (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                Position = Vector2.Lerp(_originalPos, DestinationPosition, _lerpVal);
                if (_lerpVal >= 1)
                {
                    _moving = false;
                    Position = DestinationPosition;
                    EntityEventArgs args = new EntityEventArgs
                    {
                        MapCoord = World.GetMapCoordFromPosition(Position)
                    };
                    if (LandedTile != null) LandedTile(this, args);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(Sprite);
        }

        public event EventHandler<EntityEventArgs> LandedTile;

        public string Name { get; set; }
        public bool IsDead { get; set; }
        public World World { get { return Manager.World; } }
        public Sprite Sprite { get; set; }
        public Vector2 Position 
        { 
            get { return Sprite.Position; }
            set { Sprite.Position = value; }     
        }

        float _timeToMove = 2f;
        float _lerpVal = 0;
        bool _moving = false;

        Vector2 _originalPos = Vector2.Zero;
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
                _lerpVal = 0;
                _moving = true;
            }
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
