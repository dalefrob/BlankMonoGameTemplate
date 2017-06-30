﻿using BlankMonoGameTemplate.Engine.Data;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BlankMonoGameTemplate.Engine
{
    public static class Helper
    {
        #region Loaders
        public static void SaveTilesetData(TilesetTemplate tilesetData)
        {
            XmlSerializer x = new XmlSerializer(typeof(TilesetTemplate));
            using (FileStream fs = new FileStream(tilesetData.Name + ".xml", FileMode.Create))
            {
                // do something with the file stream here
                x.Serialize(fs, tilesetData);
                fs.Close();
            }
        }

        public static TilesetTemplate LoadTilesetData(string filename)
        {
            TilesetTemplate _tilesetData;

            XmlSerializer x = new XmlSerializer(typeof(TilesetTemplate));
            using (FileStream fs = new FileStream(filename + ".xml", FileMode.OpenOrCreate))
            {
                // do something with the file stream here
                _tilesetData = (TilesetTemplate)x.Deserialize(fs);
                fs.Close();
            }

            return _tilesetData;
        }

        public static void SaveMapData(MapData gameMap, string filename)
        {
            XmlSerializer x = new XmlSerializer(typeof(MapData));
            using (FileStream fs = new FileStream(filename + ".xml", FileMode.Create))
            {
                // do something with the file stream here
                x.Serialize(fs, gameMap);
                fs.Close();
            }
        }

        public static MapData LoadMapData(ContentManager content, string filename)
        {
            MapData _mapData;
            try
            {
                XmlSerializer x = new XmlSerializer(typeof(MapData));
                using (FileStream fs = new FileStream(filename + ".xml", FileMode.OpenOrCreate))
                {
                    // do something with the file stream here
                    _mapData = (MapData)x.Deserialize(fs);
                    fs.Close();
                }
            } 
            catch (Exception ex)
            {
                _mapData = new MapData(16, 16, 16, "Floor");
                _mapData.Layers.Add(new MapLayer(_mapData, MapLayer.LayerType.Tile) { TilesetName = "Wall" });
            }
          
            return _mapData;
        }
        
        #endregion

        public static int Index2dTo1d(int xMax, int x, int y)
        {
            return (y * xMax) + x;
        }
    }
}
