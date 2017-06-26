using Microsoft.Xna.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Engine.Entities
{
    public class Monster : Character, IBlocker
    {
        public Monster() : base()
        {
            Name = "Monster";
            Sprite.TextureRegion = WorldScreen.Textures2D["Slime0"].GetRegion(0);
            Player.PlayerMoved += Player_PlayerMoved;
        }

        void Player_PlayerMoved(object sender, EventArgs e)
        {
            var directions = (Direction[])Enum.GetValues(typeof(Direction));
            var randomDirection = directions[Manager.random.Next(directions.Length)];
            Move(randomDirection, 1);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override bool CanMove(Point newCoord)
        {
            // Check basic map bounds and collisions
            var result = base.CanMove(newCoord);
            // Check entities that may obstruct
            var entities = World.GetEntitiesAtCoord(newCoord).ToList();
            if (entities.OfType<IBlocker>().Any())
            {
                result = false;
            }

            return result;
        }

        #region AI

        bool seenPlayer = false;
        bool adjacentToPlayer = false;

        void ScanRange()
        {

        }

        #endregion

        public int ViewRange { get; set; }
        public int TouchDamage { get; set; }
        public bool CanGoThruWalls { get; set; }
        
    }
}
