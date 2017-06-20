using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace BlankMonoGameTemplate.Engine
{
    public class Player : SimpleDrawableGameComponent, IMovable
    {
        public Player(World world, bool localPlayer) 
        {
            World = world;
            LocalPlayer = localPlayer;
            spriteBatch = new SpriteBatch(World.Game.GraphicsDevice);

        }

        public Player(Sprite sprite)
        {
            Sprite = sprite;

        }

        public override void Initialize()
        {
            
            base.Initialize();
        }

		public override void Draw(GameTime gameTime)
		{
            if (Sprite == null) return;
            spriteBatch.Begin();
            spriteBatch.Draw(Sprite);
            spriteBatch.End();
		}

		public override void Update(GameTime gameTime)
		{
            Velocity = Vector2.Zero;
            if (!IsEnabled) return;

            if(LocalPlayer)
            {
                KeyboardState newState = Keyboard.GetState();

                // Get this obj position
                var objMapPos = World.GetWorldPointFromPosition(this.Position);

                if (newState.IsKeyUp(Keys.Down) && oldState.IsKeyDown(Keys.Down))
                {
                    World.MoveObjectToTile(this, objMapPos + new Point(0, 1));
				}
				else if (newState.IsKeyUp(Keys.Right) && oldState.IsKeyDown(Keys.Right))
				{
                    World.MoveObjectToTile(this, objMapPos + new Point(1, 0));
				}
				else if (newState.IsKeyUp(Keys.Up) && oldState.IsKeyDown(Keys.Up))
				{
					World.MoveObjectToTile(this, objMapPos + new Point(0, -1));
				}
				else if (newState.IsKeyUp(Keys.Left) && oldState.IsKeyDown(Keys.Left))
				{
					World.MoveObjectToTile(this, objMapPos + new Point(-1, 0));
				}

                oldState = newState;
            }

            Position += Velocity;
		} 

        public bool LocalPlayer { get; private set; }

        public Vector2 Velocity { get; set; }

        public Sprite Sprite { get; set; }
        public Vector2 Position
        {
            get { return Sprite.Position; }
            set { Sprite.Position = value; }
        }

        public Point CurrentMapTile(Map map)
        {
            return (Position / map.TileSize).ToPoint();
        }

        public World World
        {
            get;
            private set;
        }

        KeyboardState oldState;
        SpriteBatch spriteBatch;

        /*
        public void SmoothMove() 
        {
			KeyboardState newState = Keyboard.GetState();

			// Smooth Movement
			float velX = 0;
			float velY = 0;
			if (newState.IsKeyDown(Keys.Down))
			{
				velY = 1;
			}
			if (oldState.IsKeyDown(Keys.Right))
			{
				velX = 1;
			}
			if (newState.IsKeyDown(Keys.Up))
			{
				velY = -1;
			}
			if (oldState.IsKeyDown(Keys.Left))
			{
				velX = -1;
			}
			Velocity = new Vector2(velX, velY);

			oldState = newState;
        }*/
    }
}
