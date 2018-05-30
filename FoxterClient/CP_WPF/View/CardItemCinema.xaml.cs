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
using ClassLibrary;

namespace CP_WPF.View
{
    /// <summary>
    /// Логика взаимодействия для CardItemCinema.xaml
    /// </summary>
    public partial class CardItemCinema : UserControl
    {
        MainMenuxaml win;
        Cinema cinema;
        public CardItemCinema(MainMenuxaml win, Cinema cinema)
        {
            this.win = win;
            InitializeComponent();

            this.PlaceNameCinema.Text = cinema.Name ?? "";
            this.PlaceCinama.Text = cinema.Address ?? "";
            this.cinema = cinema;
        }

        private void DetailsCinema_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
