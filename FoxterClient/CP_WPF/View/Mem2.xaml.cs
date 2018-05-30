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
    /// Логика взаимодействия для Mem2.xaml
    /// </summary>
    public partial class Mem2 : UserControl
    {
        Window1 win;
        public Mem2(Window1 win)
        {
            this.win = win;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            win.gridInfo.Children.Clear();
            win.gridInfo.Children.Add(new Mem(win));
        }
    }
}
