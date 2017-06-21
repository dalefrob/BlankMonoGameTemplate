using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Screens
{
    public class GameScreen : Screen
    {
        public virtual void OnShow() { }

        public new void Show()
        {
            if (!IsInitialized)
                Initialize();

            IsVisible = true;
            OnShow();
        }
    }
}
