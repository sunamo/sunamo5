using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class GlobalKeyboardHookEventArgs : HandledEventArgs
    {
        public GlobalKeyboardHook.KeyboardState KeyboardState { get; private set; }
        public LowLevelKeyboardInputEvent KeyboardData { get; private set; }

        public GlobalKeyboardHookEventArgs(
            LowLevelKeyboardInputEvent keyboardData,
            GlobalKeyboardHook.KeyboardState keyboardState)
        {
            KeyboardData = keyboardData;
            KeyboardState = keyboardState;
        }
    }