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
    public class MapRenderer
    {
        public MapRenderer(Game game, GameMap map)
        {
            Game = game;
            Position = Vector2.Zero;
            Map = map;

            foreach (var layer in Map.Layers)
            {
                // Load layer tilesets
                var tilesetFilename = layer.TilesetName;
                if (!Tilesets.ContainsKey(tilesetFilename))
                {
                    Tilesets.Add(tilesetFilename, Tileset.LoadFromFile(Game, tilesetFilename + ".xml"));
                }
            }
        }

        public void Update(GameTime gameTime) 
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, Point focused)
        {
			for (int l = 0; l < Map.Layers.Count; l++) // Layer
			{
	            for (var i = 0; i < Map.Height; i++) // X coord
	            {
	                for (var j = 0; j < Map.Width; j++) // Y coord
	                {	                    
	                    var tileId = Map.GetTileAt(l, i, j);
	                    var tileTexture = Tilesets[Map.Layers[l].TilesetName].GetTile(tileId).Texture;
                        var color = (Equals(new Point(i, j), focused) && Debug) ? Color.Red : Color.White;
                        var destinationRect = new Rectangle
                        {
                            X = (int)Position.X + Map.TileSize * i,
                            Y = (int)Position.Y + Map.TileSize * j,
                            Width = Map.TileSize,
                            Height = Map.TileSize
                        };
	                    spriteBatch.Draw(tileTexture, destinationRect, color);	                    
	                }
	            }
            };           
        }

        public Vector2 Position { get; set; }
        public bool Debug { get; set; } 

        Dictionary<string, Tileset> Tilesets = new Dictionary<string, Tileset>();

        public Game Game
        {
            get;
            private set;
        }

        public GameMap Map
        {
            get;
            set;
        }
          
    }
}
