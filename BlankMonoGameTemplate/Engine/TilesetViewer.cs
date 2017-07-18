﻿using System;
using System.Linq;
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

            var tileSize = Tileset.Tilesize;

            for (int i = 0; i < TileSlots.Count; i++)
            {
				var x = i % TileSlotsHorizontal;
				var y = i / TileSlotsHorizontal;

                var _tileSlot = TileSlots[i];
				var color = Equals(_tileSlot, SelectedTileSlot) ? Color.Red : Color.White;
				
				var destinationRect = new Rectangle
				{
					X = (int)(Position.X + ScrollOffsetPosition.X) + tileSize * x,
					Y = (int)(Position.Y + ScrollOffsetPosition.Y) + tileSize * y,
					Width = tileSize,
					Height = tileSize
				};

                spriteBatch.Draw(_tileSlot.Tile.TextureRegion, destinationRect, color);
                spriteBatch.DrawRectangle(destinationRect, Color.DimGray, 0.5f);               
            }
            var fullRect = new Rectangle
			{
				X = (int)Position.X,
				Y = (int)Position.Y,
				Width = TileSlotsHorizontal * tileSize,
				Height = (TileSlots.Count / TileSlotsHorizontal) * tileSize
			};
            spriteBatch.DrawRectangle(fullRect, Color.Black);
        }

        public TileSlot SelectTileSlotFromPosition(Vector2 screenPosition)
        {
            var relativePosition = screenPosition - (Position + ScrollOffsetPosition);
            if (relativePosition.X >= 0 && relativePosition.Y >= 0)
            {
                var coordinate = (relativePosition / Tileset.Tilesize).ToPoint();
                SelectedTileSlot = TileSlots.Where(t => t.ViewerCoord == coordinate).First();
            }
            return SelectedTileSlot;
        }

        public TileSlot SelectedTileSlot { get; set; }
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

                for (int i = 0; i < value.TotalTiles; i++)
                {
					var x = i % TileSlotsHorizontal;
					var y = i / TileSlotsHorizontal;
                    var _tile = value.GetTile(i);

                    var tileSlot = new TileSlot
                    {
                        Tile = _tile,
                        ViewerCoord = new Point(x, y)
                    };

                    TileSlots.Add(tileSlot);
                }
                // Set default selected
                SelectedTileSlot = TileSlots[0];
            }
        }

        public bool IsTilesetLoaded
        {
            get { return (_tileset != null); }
        }
          
    }

    public class TileSlot 
    {
        /// <summary>
        /// The tile information
        /// </summary>
        public Tile Tile { get; set; }
        /// <summary>
        /// The location of this tile "slot" in the viewer editor
        /// </summary>
        public Point ViewerCoord { get; set; }

        public TileSlot()
        {
            
        }
    }
}
