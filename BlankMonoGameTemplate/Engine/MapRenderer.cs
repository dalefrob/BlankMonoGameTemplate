using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Sprites;
using System.Xml.Serialization;
using System.IO;

namespace BlankMonoGameTemplate.Engine
{
    /// <summary>
    /// Renders a map and its layers with the appropriate tileset
    /// </summary>
    public class MapRenderer
    {
        public MapRenderer(Map _map) 
        {
            Map = _map;
        }

        public void Update(GameTime gameTime) 
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var layerNames = Map.Layers.Keys.ToList();
            for (int l = 0; l < layerNames.Count; l++) // Layer
			{
                for (var y = 0; y < Map.Height; y++) // X coord
	            {
                    for (var x = 0; x < Map.Width; x++) // Y coord
	                {
                        var tile = Map.GetTileAt(y, x, layerNames[l]);
                        tile.Position = (Position + OffsetPosition) + new Vector2(Map.Tilesize * y, Map.Tilesize * x);
                        spriteBatch.Draw(tile);	                    
	                }
	            }
            };
        }

        public Map Map { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 OffsetPosition { get; set; }
        public Player PlayerToFollow { get; set; }
        public bool Debug { get; set; }

        public Vector2 ScreenPositionFromMapCoord(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
