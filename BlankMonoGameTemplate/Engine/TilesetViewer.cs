using System;
using System.Collections.Generic;
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

            for (int i = 0; i < TileSlots.Count; i++)
            {
				var x = i % TileSlotsHorizontal;
				var y = i / TileSlotsHorizontal;
                var tileSize = Tileset.Data.TileSize;

                var _tileSlot = TileSlots[i];
				var color = (Equals(new Point(x, y), SelectedTileCoord)) ? Color.Red : Color.White;
				
				var destinationRect = new Rectangle
				{
					X = (int)Position.X + tileSize * x,
					Y = (int)Position.Y + tileSize * y,
					Width = tileSize,
					Height = tileSize
				};

                spriteBatch.Draw(_tileSlot.Texture, destinationRect, color);
                //spriteBatch.DrawString(Game1.Mainfont, string.Format("{0}", (y * Tileset.TilesHorizontal) + x), new Vector2(destinationRect.Left, destinationRect.Top), Color.Lime);
            }

            /*// The old way!
            for (int y = 0; y < Tileset.TilesVertical; y++)
            {
                for (int x = 0; x < Tileset.TilesHorizontal; x++)
                {
                    var color = (Equals(new Point(x, y), SelectedTileCoord)) ? Color.Red : Color.White;
                    var destinationRect = new Rectangle(Position.ToPoint() + new Point(x * Tileset.Data.TileSize, y * Tileset.Data.TileSize), new Point(Tileset.Data.TileSize));
                    spriteBatch.Draw(Tileset.TileTextureByCoord(x, y), destinationRect, color);
                    spriteBatch.DrawString(Game1.Mainfont, string.Format("{0}", (y * Tileset.TilesHorizontal) + x), new Vector2(destinationRect.Left, destinationRect.Top), Color.Lime);
                }
            }
            */
        }

        public List<TileSlot> TileSlots = new List<TileSlot>();

        public Vector2 Position { get; set; }
        public Vector2 ScrollOffsetPosition { get; set; }

        int _tileSlotsHorizontal = 24;
        public int TileSlotsHorizontal
        {
            get
            {
                return _tileSlotsHorizontal;
            }
            set
            {
                _tileSlotsHorizontal = value;    
            }
        }

        Tileset _tileset;
        public Tileset Tileset
        { 
            get 
            {
                return _tileset;    
            }
            set
            {
                _tileset = value;
                // Create a new set of tileslots to show
                TileSlots.Clear();
                for (int i = 0; i < Tileset.TilesHorizontal * Tileset.TilesVertical; i++)
                {
					var x = i % TileSlotsHorizontal;
					var y = i / TileSlotsHorizontal;
                    var tileSlot = new TileSlot
                    {
                        Tile = value.GetTileData(i),
                        Texture = value.TileTextureByIndex(i),
                        ViewerCoord = new Point(x, y)
                    };
                    TileSlots.Add(tileSlot);
                }
            }
        }

        public bool IsTilesetLoaded
        {
            get { return (_tileset != null); }
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

    public class TileSlot 
    {
        public string TilesetName { get; set; }
        public Tile Tile { get; set; }
        public TextureRegion2D Texture { get; set; }
        public Point ViewerCoord { get; set; }

        public TileSlot()
        {
            
        }
    }
}
