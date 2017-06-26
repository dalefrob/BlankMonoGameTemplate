using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;

namespace BlankMonoGameTemplate.Engine.Entities
{
    public class HealthBar : Sprite
    {
        public HealthBar(int maxValue, TextureRegion2D texture) : base(texture)
        {
            Origin = Vector2.Zero;
            MaxValue = maxValue;
            Value = maxValue;
        }

        public int MaxValue { get; set; }
        public int Value { get; set; }
    }
}
