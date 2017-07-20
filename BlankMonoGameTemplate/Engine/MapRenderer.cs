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
using BlankMonoGameTemplate.Engine.Data.Map;

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
            var layers = Map.GetLayersOfType(LayerType.Texture);
            for (int l = 0; l < layers.Count; l++)
			{
                for (var y = 0; y < Map.Height; y++)
	            {
                    for (var x = 0; x < Map.Width; x++) 
	                {
                        var tile = Tileset.GetTileset("Default").GetTile(Map.Layers[l].IDArray[x, y]);
                        tile.Position = new Vector2(x * Map.Tilesize, y * Map.Tilesize);
                        spriteBatch.Draw(tile);
	                }
	            }
            };
        }

        public Map Map { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 OffsetPosition { get; set; }
        public bool Debug { get; set; }

        public Vector2 ScreenPositionFromMapCoord(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
