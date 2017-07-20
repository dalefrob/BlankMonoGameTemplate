using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Entities
{
    public interface IBlocker
    {

    }

    public interface IDoor : IBlocker
    {
        bool IsOpen { get; set; }
        bool RequiresKey { get; set; }
        void Open();
        void Close();
    }

    public class Door : Entity, IDoor // IBlocker, ISolid as a way to stop player movement?
    {
        public Door()
        {
            Name = "Door";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Open()
        {
            Sprite.TextureRegion = openTexture;
            IsOpen = true;
        }

        public void Close() { }

        public bool IsOpen { get; set; }
        public bool RequiresKey { get; set; }
        TextureRegion2D openTexture;
    }
}
