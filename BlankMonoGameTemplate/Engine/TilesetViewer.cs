using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;

namespace BlankMonoGameTemplate.Engine
{
    public class TilesetViewer : IDrawable
    {
        public TilesetViewer(GraphicsDevice graphics, Tileset tileset)
        {
            Tileset = tileset;
            spriteBatch = new SpriteBatch(graphics);
        }

        readonly SpriteBatch spriteBatch;

        public Vector2 ScreenPosition { get; set; }
        public Tileset Tileset { get; set; }

        public int DrawOrder { get; set; }
        public bool Visible { get; set; }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
			for (int y = 0; y < Tileset.TilesVertical; y++)
			{
				for (int x = 0; x < Tileset.TilesHorizontal; x++)
				{
                    var destinationRect = new Rectangle(ScreenPosition.ToPoint(), new Point(x * Tileset.TileSize, y * Tileset.TileSize));
                    spriteBatch.Draw(Tileset.GetTile(x, y).Texture, destinationRect, Color.White);
				}
			}
            spriteBatch.End();
        }
    }
}
