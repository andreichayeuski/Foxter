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

            SaveCommand = new RoutedUICommand("Save", "SaveCommand", typeof(UserCommands), inputs);
        }

        public static RoutedCommand SaveCommand { get; private set; }
    }
}
