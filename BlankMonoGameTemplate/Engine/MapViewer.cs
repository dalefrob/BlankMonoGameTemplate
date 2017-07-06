﻿using System;
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

        public void Draw(Map map, GameTime gameTime)
        {
            if (map == null) return;
            var layerNames = map.Layers.Keys.ToList();
            for (int l = 0; l < layerNames.Count; l++) // Layer
			{
                spriteBatch.Begin();
                for (var i = 0; i < map.Height; i++) // X coord
	            {
                    for (var j = 0; j < map.Width; j++) // Y coord
	                {
                        var tile = map.GetTileAt(i, j, layerNames[l]);
                        var destinationRect = new Rectangle
                        {
                            X = (int)Position.X + map.Tilesize * i,
                            Y = (int)Position.Y + map.Tilesize * j,
                            Width = map.Tilesize,
                            Height = map.Tilesize
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
