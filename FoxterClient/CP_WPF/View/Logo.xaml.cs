using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CP_WPF.View
{
    /// <summary>
    /// Логика взаимодействия для Logo.xaml
    /// </summary>
    public partial class Logo : UserControl
    {
        MainWindow win;
        public Logo(MainWindow win)
        {
            this.win = win;
            InitializeComponent();
        }
        
        private void ButtonMainMenuOpen_Click(object sender, RoutedEventArgs e)
        {
            MainMenuxaml mainMenuxaml = new MainMenuxaml(win);
            win.Close();
            mainMenuxaml.Show();
        }
    }
}
