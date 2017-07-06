using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.TextureAtlases;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Xna.Framework.Content;
using System.Threading.Tasks;
using BlankMonoGameTemplate.Engine.Data;

namespace BlankMonoGameTemplate.Engine
{
    /// <summary>
    /// A template class for map allowing import / export
    /// </summary>
    public class MapTemplate
    {
        #region XML

        public MapTemplate() { }
        public MapTemplate(string _name, int _width, int _height, int _tileSize) 
        {
            Name = _name;
            Width = _width;
            Height = _height;
            TileSize = _tileSize;
            // Add a default layer
            var newLayer = new LayerTemplate();
            LayerTemplates.Add(newLayer);           
        }

        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int TileSize { get; set; }

        public List<LayerTemplate> LayerTemplates = new List<LayerTemplate>();

        #endregion
          
        public void JumbleTiles(int maxRandomValue) {
            var random = new Random();
            foreach (var layer in LayerTemplates)
            {
                for (int i = 0; i < layer.TileTemplateIds.Count(); i++)
                {
                    layer.TileTemplateIds[i] = random.Next(maxRandomValue);
                }
            }
        }    
    }
}
