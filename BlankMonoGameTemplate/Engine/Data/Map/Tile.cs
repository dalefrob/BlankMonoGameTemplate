using Microsoft.Xna.Framework.Graphics;
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
    public class Tile
    {
        public TextureRegion2D Texture { get; internal set; }

        public int WorldID { get; set; }
        public int TemplateID { get; set; }

        public int LightLevel { get; set; }
        public bool Obstacle { get; set; }

        public Action OnPlayerLanded { get; set; }

        public Tile(TextureRegion2D texture = null)
        {
            Texture = texture;
        }

        public Tile LoadTemplate(TileTemplate _template)
        {
            Obstacle = _template.TileFlags.HasFlag(MovementFlags.Obstacle);
            return this;
        }

        public static Tile Default()
        {
            return new Tile
            {
                WorldID = 0,
                TemplateID = 0,
                Texture = null,
                LightLevel = 0,
                Obstacle = false,
                OnPlayerLanded = null
            };
        }

        public static TextureRegion2D BlankRegion(int size)
        {
            var blankTexture = new Texture2D(GameServices.GetService<GraphicsDevice>(), size, size);
            var region = new TextureRegion2D(blankTexture);
            return region;
        }
    }
}
