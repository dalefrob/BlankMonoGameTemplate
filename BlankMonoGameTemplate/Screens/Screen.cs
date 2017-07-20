using BlankMonoGameTemplate.Engine;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Screens
{
    public abstract class Screen : IUpdate
    {
        public Screen()
        {
            
        }

        public virtual void Initialize() { }
        public virtual void LoadContent() { }
        public virtual void UnloadContent() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }


        public virtual void Activate()
        {
            Visible = true;
        }

        public virtual void Deactivate()
        {
            Visible = false;
        }

        public ScreenManagerComponent ScreenManager { get; internal set; }
        public Game Game
        {
            get { return ScreenManager.Game; }
        }

        protected List<IGameManager> Managers = new List<IGameManager>();
        public T GetManager<T>()
        {
            return Managers.OfType<T>().FirstOrDefault();
        }

        public bool Visible { get; set; }
        public bool Translucent { get; set; }
    }
}
