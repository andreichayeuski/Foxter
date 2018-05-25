using System.Windows.Input;

namespace FoxterServer_WPF
{
    public static class UserCommands
    {
        static UserCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection
            {
                new KeyGesture(Key.S, ModifierKeys.Control, "Ctrl+S")
            };

            SomeCommand = new RoutedUICommand("Some", "SomeCommand", typeof(UserCommands), inputs);
        }

        public static RoutedCommand SomeCommand { get; private set; }
    }
}
