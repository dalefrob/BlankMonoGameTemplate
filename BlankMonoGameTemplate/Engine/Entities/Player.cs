using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using BlankMonoGameTemplate.Engine.Entities;

namespace BlankMonoGameTemplate.Engine
{
    public class Player : Character
    {
        public Player() 
        {
            Name = "Default Player";
            Sprite.TextureRegion = WorldScreen.Textures2D["Player0"].GetRegion(0);
            this.LandedTile += Player_LandedTile;
        }

        void Player_LandedTile(object sender, EntityEventArgs e)
        {
            var entities = World.GetEntitiesAtCoord(e.MapCoord).ToList();
            foreach(var c in entities.OfType<ICollectible>())
            {
                c.Collect();
            }
        }

		public override void Update(GameTime gameTime)
		{ 
            if(LocalPlayer)
            {
                KeyboardState newState = Keyboard.GetState();

                // Get this obj position
                var objMapPos = World.GetMapCoordFromPosition(this.Position);

                if (newState.IsKeyUp(Keys.Down) && oldState.IsKeyDown(Keys.Down))
                {
                    Move(Direction.Down);
				}
				else if (newState.IsKeyUp(Keys.Right) && oldState.IsKeyDown(Keys.Right))
				{
                    Move(Direction.Right);
				}
				else if (newState.IsKeyUp(Keys.Up) && oldState.IsKeyDown(Keys.Up))
				{
                    Move(Direction.Up);
				}
				else if (newState.IsKeyUp(Keys.Left) && oldState.IsKeyDown(Keys.Left))
				{
                    Move(Direction.Left);
				}               
                else if (newState.IsKeyUp(Keys.Space) && oldState.IsKeyDown(Keys.Space)) // "USE" key
                {

                }

                oldState = newState;
            }
            base.Update(gameTime);
		}

        public override void Move(Direction direction, int tileCount = 1)
        {
            base.Move(direction, tileCount);
            if (PlayerMoved != null) PlayerMoved(this, EventArgs.Empty);
        }

        protected override bool CanMove(Point newCoord)
        {
            // Check basic map bounds and collisions
            var result = base.CanMove(newCoord);
            // Check entities that may obstruct
            var entities = World.GetEntitiesAtCoord(newCoord).ToList();
            if (entities.OfType<IDoor>().Any())
            {
                var door = entities.OfType<IDoor>().First();
                if (!door.IsOpen)
                {
                    if (PlayerData.Instance.Keys > 0)
                    {
                        door.Open();
                        PlayerData.Instance.Keys--;
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        public bool LocalPlayer { get; set; }

        public static event EventHandler<EventArgs> PlayerMoved;

        KeyboardState oldState;

    }
}
