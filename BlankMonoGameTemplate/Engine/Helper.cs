using BlankMonoGameTemplate.Engine.Data;
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
using Newtonsoft.Json;

namespace BlankMonoGameTemplate.Engine
{
    public static class Helper
    {
        #region Loaders
        public static void SaveTileset(Tileset _tileset)
        {
            var jsonResult = JsonConvert.SerializeObject(_tileset, Formatting.Indented);
            File.WriteAllText(_tileset.Name + ".json", jsonResult);
            /*
            XmlSerializer x = new XmlSerializer(typeof(Tileset));
            using (FileStream fs = new FileStream(_tileset.Name + ".xml", FileMode.Create))
            {
                // do something with the file stream here
                x.Serialize(fs, _tileset);
                fs.Close();
            }*/
        }

        public static Tileset LoadTileset(string filename)
        {
            var json = File.ReadAllText(filename + ".json");
            Tileset _tileset = JsonConvert.DeserializeObject<Tileset>(json);
            _tileset.BuildAtlases();
            /*
            XmlSerializer x = new XmlSerializer(typeof(Tileset));
            using (FileStream fs = new FileStream(filename + ".xml", FileMode.OpenOrCreate))
            {
                // do something with the file stream here
                _tilesetData = (Tileset)x.Deserialize(fs);
                fs.Close();
            }
            */
            return _tileset;
        }

        public static void SaveMap(Map _map)
        {
            var jsonResult = JsonConvert.SerializeObject(_map, Formatting.Indented);
            File.WriteAllText(_map.Name + ".json", jsonResult);
            /*
            XmlSerializer x = new XmlSerializer(typeof(Map));
            using (FileStream fs = new FileStream(_map.Name + ".xml", FileMode.Create))
            {
                // do something with the file stream here
                x.Serialize(fs, _map);             
                fs.Close();
            }
             */
        }

        public static Map LoadMap(ContentManager content, string filename)
        {
            var json = File.ReadAllText(filename + ".json");
            Map _map = JsonConvert.DeserializeObject<Map>(json);
            _map.Build();
            /*
            XmlSerializer x = new XmlSerializer(typeof(Map));
            using (FileStream fs = new FileStream(filename + ".xml", FileMode.OpenOrCreate))
            {
                // do something with the file stream here
                _mapData = (Map)x.Deserialize(fs);
                fs.Close();
            }
                       * */
            return _map;
        }
        
        #endregion

        public static int Index2dTo1d(int xMax, int x, int y)
        {
            return (y * xMax) + x;
        }
    }
}
