using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace planecatch
{
    public class InputHelper : GameComponent
    {
        private KeyboardState _lastState;
        private KeyboardState _currentState;

        public InputHelper(Game game) : base(game)
        {
        }

        public void ExecuteIf(Func<KeyboardState, bool> condition, Action action)
        {
            if (condition(_currentState))
                action();
        }

        public void ExecuteIfKeyPressed(Keys k, Action action)
        {
            if (_currentState.IsKeyDown(k))
                action();
        }

        public void ExecuteIfJustPressed(Keys key, Action action)
        {
            if (_lastState.IsKeyUp(key) && _currentState.IsKeyDown(key))
                action();
        }

        public override void Update(GameTime gameTime)
        {
            _lastState = _currentState;
            _currentState = Keyboard.GetState();

            base.Update(gameTime);
        }
    }
}
