using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Entities
{
    public class Key : Entity, ICollectible
    {
        public Key()
        {
            Name = "Key";
        }

        public void Collect()
        {
            if (!Collected)
            {
                Collected = true;
                Sprite.IsVisible = false;
                Console.WriteLine("Collected: Key");
                PlayerData.Instance.Keys++;
            }
        }

        public bool Collected { get; set; }
        public bool IsUnique { get; set; }
    }
}
