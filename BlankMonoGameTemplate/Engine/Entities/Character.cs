using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Entities
{
    public class Character : Entity, IMovable
    {
        public override void Update(GameTime gameTime)
        {
            // Smooth move to destination tile
            if (moving)
            {
                lerpVal += timeToMove / (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                Position = Vector2.Lerp(originalPos, DestinationPosition, lerpVal);
                if (lerpVal >= 1)
                {
                    moving = false;
                    Position = DestinationPosition;
                    EntityEventArgs args = new EntityEventArgs
                    {
                        MapCoord = World.GetMapCoordFromPosition(Position)
                    };
                    if (LandedTile != null) LandedTile(this, args);
                }
            }
        }

        public virtual void Move(Direction direction, int tileCount = 1)
        {
            if (moving) return; // TODO - change this

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
            if (CanMove(_newCoord))
            {
                DestinationPosition = World.GetPositionFromMapCoord(_newCoord);
            }
        }

        protected virtual bool CanMove(Point newCoord)
        {
            bool result = true;
            if (!(newCoord.X < 0 || newCoord.X >= World.Map.Width || newCoord.Y < 0 || newCoord.Y >= World.Map.Height))
            {
                if (World.Map.Collisions[newCoord.X, newCoord.Y])
                {
                    // Collision at next point
                    result = false;
                }
            }
            else
            {
                // Outside map range
                result = false;
            }          
            return result;
        }

        public virtual event EventHandler<EntityEventArgs> LandedTile;

        float timeToMove = 2f;
        float lerpVal = 0;
        bool moving = false;
        Vector2 originalPos = Vector2.Zero;
        Vector2 _destinationPos;
        public Vector2 DestinationPosition
        {
            get
            {
                return _destinationPos;
            }
            set
            {
                _destinationPos = value;
                originalPos = Position;
                lerpVal = 0;
                moving = true;
            }
        }
    }
}
