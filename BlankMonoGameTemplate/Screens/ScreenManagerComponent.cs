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
            if (!_screens.OfType<T>().Any())
            {
                // Mark this as the first screen added as a fallback
                if (_screens.Count == 0)
                {
                    _firstScreen = newScreen;
                }

                newScreen.ScreenManager = this;

                newScreen.Initialize();
                newScreen.LoadContent();
                newScreen.Translucent = translucent;
              
                if (activateNow)
                {
                    // Put the new screen at position 0
                    _screens.Insert(0, newScreen);
                    newScreen.Activate();
                }
                else
                {
                    _screens.Add(newScreen);
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
            _screens.Remove(screenToRemove);
        }

        internal void DeleteScreen(Screen screen)
        {
            _screens.Remove(screen);
            screen.UnloadContent();
            screen = null;
        }

        public T GetScreen<T>() where T : Screen
        {
            var result = (T)_screens.OfType<T>().First();
            return result;
        }

        public void ChangeScreen<T>(bool removeCurrent = false) where T : Screen
        {
            // Try and get the next screen
            Screen nextScreen = null;
            foreach (var screen in _screens)
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
                _screens.Remove(nextScreen);
                _screens.Insert(0, nextScreen);

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
            for (int i = 0; i < _screens.Count; i++)
            {
                // Not visible? Skip this iteration
                if (!_screens[i].Visible)
                {
                    continue;
                }

                _screens[i].Update(gameTime);

                // Not see-through? Stop drawing 
                if (!_screens[i].Translucent)
                {
                    break;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < _screens.Count; i++)
            {
                // Not visible? Skip this iteration
                if (!_screens[i].Visible)
                {
                    continue;
                }

                _screens[i].Draw(gameTime);

                // Not see-through? Stop drawing 
                if (!_screens[i].Translucent)
                {
                    break;
                }
            }
        }

        public Screen ActiveScreen
        {
            get { return _screens[0]; }
        }

        Screen _firstScreen;
        List<Screen> _screens = new List<Screen>();
        public List<Screen> Screens
        {
            get
            {
                return _screens;
            }
        }

    }
}
