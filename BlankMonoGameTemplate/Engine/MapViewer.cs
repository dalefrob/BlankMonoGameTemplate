using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.TextureAtlases;
using System.Xml.Serialization;
using System.IO;

namespace BlankMonoGameTemplate.Engine
{
    /// <summary>
    /// Renders a map and its layers with the appropriate tileset
    /// </summary>
    public class MapViewer
    {
        public MapViewer() 
        {
            spriteBatch = new SpriteBatch(GameServices.GetService<GraphicsDevice>());
        }

        public void Update(GameTime gameTime) 
        {
            
        }

        public void Draw(MapTemplate map, GameTime gameTime)
        {
            if (map == null) return;
            for (int l = 0; l < map.Layers.Count; l++) // Layer
			{
                spriteBatch.Begin();
                for (var i = 0; i < map.Height; i++) // X coord
	            {
                    for (var j = 0; j < map.Width; j++) // Y coord
	                {
                        var tileId = map.GetTileIdAt(l, i, j);
                        var tile = Tileset.loadedTilesets[map.Layers[l].TilesetName].GetTile(tileId);
                        var destinationRect = new Rectangle
                        {
                            X = (int)Position.X + map.TileSize * i,
                            Y = (int)Position.Y + map.TileSize * j,
                            Width = map.TileSize,
                            Height = map.TileSize
                        };
                        spriteBatch.Draw(tile.Texture, destinationRect, Color.White);	                    
	                }
	            }
                spriteBatch.End();
            };           
        }

        public Vector2 Position { get; set; }
        public bool Debug { get; set; }

        SpriteBatch spriteBatch;
    }
}
