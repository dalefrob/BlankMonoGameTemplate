using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended;

namespace BlankMonoGameTemplate.Engine
{
    public class TilesetViewer : IDrawable
    {
        public TilesetViewer(SpriteBatch spriteBatch, GraphicsDevice graphics, Tileset tileset)
        {
            Tileset = tileset;
            this.spriteBatch = spriteBatch;
        }

        public void Draw(GameTime gameTime)
        {
            for (int y = 0; y < Tileset.TilesVertical; y++)
            {
                for (int x = 0; x < Tileset.TilesHorizontal; x++)
                {
                    var color = (Equals(new Point(x, y), SelectedTile)) ? Color.Red : Color.White;
                    var destinationRect = new Rectangle(ScreenPosition.ToPoint() + new Point(x * Tileset.TileSize, y * Tileset.TileSize), new Point(Tileset.TileSize));
                    spriteBatch.Draw(Tileset.GetTile(x, y).Texture, destinationRect, color);
                }
            }
        }

        SpriteBatch spriteBatch;

        public Vector2 ScreenPosition { get; set; }
        public Vector2 ScrollOffsetPosition { get; set; }
        public Tileset Tileset { get; set; }

        Point _selectedTile = Point.Zero;
        public event EventHandler<TileViewerEventArgs> SelectionChanged;
        public Point SelectedTile
        {
            get { return _selectedTile; }
            set
            {
                _selectedTile = value;
                var eventArgs =  new TileViewerEventArgs() { SelectedTile = Tileset.GetTile(value.X, value.Y) };
                if (SelectionChanged != null) SelectionChanged(this, eventArgs);
            }
        }

        public int DrawOrder { get; set; }
        public bool Visible { get; set; }

        public event EventHandler<EventArgs> DrawOrderChanged;
        public event EventHandler<EventArgs> VisibleChanged;
    }

    public class TileViewerEventArgs : EventArgs
    {
        public Tile SelectedTile { get; set; }
        public TileViewerEventArgs() : base() { }
    }
}
