using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace planecatch
{
    public static class InputHelper
    {
        public static void ExecuteIf(Func<KeyboardState, bool> condition, Action action)
        {
            if (condition(Keyboard.GetState()))
                action();
        }

        public static void ExecuteIfKeyPressed(Keys k, Action action)
        {
            if (Keyboard.GetState().IsKeyDown(k))
                action();
        }
    }
}
