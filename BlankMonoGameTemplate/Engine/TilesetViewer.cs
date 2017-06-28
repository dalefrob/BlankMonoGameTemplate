using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using BlankMonoGameTemplate.Engine.Data;

namespace BlankMonoGameTemplate.Engine
{
    public class TilesetViewer
    {
        public TilesetViewer() { }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!IsTilesetLoaded) return;
            for (int y = 0; y < Tileset.TilesVertical; y++)
            {
                for (int x = 0; x < Tileset.TilesHorizontal; x++)
                {
                    var color = (Equals(new Point(x, y), SelectedTileCoord)) ? Color.Red : Color.White;
                    var destinationRect = new Rectangle(Position.ToPoint() + new Point(x * Tileset.Data.TileSize, y * Tileset.Data.TileSize), new Point(Tileset.Data.TileSize));
                    spriteBatch.Draw(Tileset.TileTextureByCoord(x, y), destinationRect, color);
                }
            }
        }

        public Vector2 Position { get; set; }
        public Vector2 ScrollOffsetPosition { get; set; }
        public Tileset Tileset { get; set; }
        public bool IsTilesetLoaded
        {
            get { return (Tileset != null); }
        }

        Point _selectedTile = Point.Zero;
        public event EventHandler<TileViewerEventArgs> SelectionChanged;
        public Point SelectedTileCoord
        {
            get { return _selectedTile; }
            set
            {
                _selectedTile = value;
                var eventArgs =  new TileViewerEventArgs() { SelectedTile = Tileset.GetTileData(value.X, value.Y) };
                if (SelectionChanged != null) SelectionChanged(this, eventArgs);
            }
        }        
    }

    public class TileViewerEventArgs : EventArgs
    {
        public Tile SelectedTile { get; set; }
        public TileViewerEventArgs() : base() { }
    }
}
