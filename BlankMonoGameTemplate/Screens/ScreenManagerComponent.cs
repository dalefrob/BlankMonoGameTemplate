using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankMonoGameTemplate.Screens
{
    public class ScreenManagerComponent : DrawableGameComponent
    {
        public ScreenManagerComponent(Game game) : base(game)
        {

        }

        public void AddScreen<T>(bool activateNow = false, bool translucent = false) where T : Screen, new()
        {
            T newScreen = new T();

            // Make sure there is no duplicate screen type
            if (!Screens.OfType<T>().Any())
            {
                // Mark this as the first screen added as a fallback
                if (Screens.Count == 0)
                {
                    _firstScreen = newScreen;
                }

                newScreen.Manager = this;

                newScreen.Initialize();
                newScreen.LoadContent();
                newScreen.Translucent = translucent;
              
                if (activateNow)
                {
                    // Put the new screen at position 0
                    Screens.Insert(0, newScreen);
                    newScreen.Activate();
                }
                else
                {
                    Screens.Add(newScreen);
                }
            }
            else
            {
                throw new Exception("You can't register two screens of the same type.");
            }
        }

        internal void RemoveScreen<T>() where T : Screen
        {
            var screenToRemove = GetScreen<T>();
            screenToRemove.UnloadContent();
            Screens.Remove(screenToRemove);
        }

        internal void DeleteScreen(Screen screen)
        {
            Screens.Remove(screen);
            screen.UnloadContent();
            screen = null;
        }

        public T GetScreen<T>() where T : Screen
        {
            var result = (T)Screens.OfType<T>().First();
            return result;
        }

        public void ChangeScreen<T>(bool removeCurrent = false) where T : Screen
        {
            // Try and get the next screen
            Screen nextScreen = null;
            foreach (var screen in Screens)
            {
                if (screen.GetType() == typeof(T))
                {
                    nextScreen = screen;
                    break;
                }
            }

            // If we got it...
            if (nextScreen != null)
            {
                var _prevScreen = ActiveScreen;
                _prevScreen.Deactivate();

                // Reinsert the next screen at position zero
                Screens.Remove(nextScreen);
                Screens.Insert(0, nextScreen);

                if (removeCurrent)
                {
                    DeleteScreen(_prevScreen);
                }

                nextScreen.Activate();
            }
            else
            {
                throw new Exception("No screen to change to.");
            }
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < Screens.Count; i++)
            {
                // Not visible? Skip this iteration
                if (!Screens[i].Visible)
                {
                    continue;
                }

                Screens[i].Update(gameTime);

                // Not see-through? Stop drawing 
                if (!Screens[i].Translucent)
                {
                    break;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < Screens.Count; i++)
            {
                // Not visible? Skip this iteration
                if (!Screens[i].Visible)
                {
                    continue;
                }

                Screens[i].Draw(gameTime);

                // Not see-through? Stop drawing 
                if (!Screens[i].Translucent)
                {
                    break;
                }
            }
        }

        public Screen ActiveScreen
        {
            get { return Screens[0]; }
        }

        Screen _firstScreen;
        List<Screen> Screens = new List<Screen>();

    }
}
