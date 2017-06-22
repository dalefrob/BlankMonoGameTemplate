using System;
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
    public class Player : Entity, IMovable
    {
        public Player(World world, bool localPlayer) : base(world)
        {
            LocalPlayer = localPlayer;
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
                    Step(Direction.Down);
				}
				else if (newState.IsKeyUp(Keys.Right) && oldState.IsKeyDown(Keys.Right))
				{
                    Step(Direction.Right);
				}
				else if (newState.IsKeyUp(Keys.Up) && oldState.IsKeyDown(Keys.Up))
				{
                    Step(Direction.Up);
				}
				else if (newState.IsKeyUp(Keys.Left) && oldState.IsKeyDown(Keys.Left))
				{
                    Step(Direction.Left);
				}

                oldState = newState;
            }

            base.Update(gameTime);
		} 

        public bool LocalPlayer { get; private set; }

        KeyboardState oldState;

    }
}
