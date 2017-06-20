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
        public MapRenderer(Game game) 
        {
            Game = game;
        }

        public MapRenderer(Game game, Map map) : this(game)
        {           
            Position = Vector2.Zero;
            Map = map;
        }

        public void Update(GameTime gameTime) 
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Map == null) return;
			for (int l = 0; l < Map.Layers.Count; l++) // Layer
			{
	            for (var i = 0; i < Map.Height; i++) // X coord
	            {
	                for (var j = 0; j < Map.Width; j++) // Y coord
	                {	                    
	                    var tileId = Map.GetTileAt(l, i, j);
	                    var tileTexture = Map.Tilesets[Map.Layers[l].TilesetName].GetTile(tileId).Texture;
                        var destinationRect = new Rectangle
                        {
                            X = (int)Position.X + Map.TileSize * i,
                            Y = (int)Position.Y + Map.TileSize * j,
                            Width = Map.TileSize,
                            Height = Map.TileSize
                        };
	                    spriteBatch.Draw(tileTexture, destinationRect, Color.White);	                    
	                }
	            }
            };           
        }

        public Vector2 Position { get; set; }
        public bool Debug { get; set; } 

        //Dictionary<string, Tileset> Tilesets = new Dictionary<string, Tileset>();

        public Game Game
        {
            get;
            private set;
        }

        public Map Map
        {
            get;
            set;    
        }
          
    }
}
