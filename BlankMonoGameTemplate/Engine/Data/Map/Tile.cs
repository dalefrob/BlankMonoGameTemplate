using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine
{
    /// <summary>
    /// Tile as represented on a map or in a tileset viewer
    /// </summary>
    public class Tile : Sprite
    {
        public Tile(int _templateId = 0, TextureRegion2D _texture = null) : base(_texture)
        {
            TemplateID = _templateId;
        }

        public Tile LoadFromModel(TileModel _model)
        {
            TemplateID = _model.ID;
            Obstacle = _model.TileFlags.HasFlag(MovementFlags.Obstacle);
            return this;
        }

        public int WorldID { get; set; }
        public int TemplateID { get; set; }

        public int LightLevel { get; set; }
        public bool Obstacle { get; set; }

        public Action OnPlayerLanded { get; set; }

        public static TextureRegion2D BlankRegion(int size)
        {
            var blankTexture = new Texture2D(GameServices.GetService<GraphicsDevice>(), size, size);
            var region = new TextureRegion2D(blankTexture);
            return region;
        }

        public static Tile[,] ListTo2dArray(int _width, int _height, List<Tile> _tiles)
        {
            Tile[,] result = new Tile[_width, _height];
            for (int i = 0; i < _tiles.Count; i++)
            {
                var x = i % _width;
                var y = i / _width;
                result[x, y] = _tiles[i];
            }

            return result;
        }
    }
}
