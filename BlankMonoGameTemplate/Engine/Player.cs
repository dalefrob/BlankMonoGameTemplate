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
        public Player() 
        {
            Name = "Default Player";
            Sprite = new Sprite(WorldScreen.Textures2D["Player0"].GetRegion(0));
            Sprite.Origin = Vector2.Zero;
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

                oldState = newState;
            }
            base.Update(gameTime);
		} 

        public bool LocalPlayer { get; set; }

        KeyboardState oldState;

    }
}
