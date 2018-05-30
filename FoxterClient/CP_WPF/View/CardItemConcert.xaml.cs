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
    /// Логика взаимодействия для CardItemConcert.xaml
    /// </summary>
    public partial class CardItemConcert : UserControl
    {
        public Concert concert;
        MainMenuxaml win;
        public bool flagFavorite = false;

        public CardItemConcert(MainMenuxaml win, Concert concert)
        {
            this.win = win;
            InitializeComponent();
            Uri urislide1 = new Uri(concert.Image_Main, UriKind.RelativeOrAbsolute);
            this.MainImageConcert.Source = BitmapFrame.Create(urislide1);
            this.EventNameConcert.Text = concert.Name ?? "";
            foreach (string s in FilmHandler.GetListOfGenres(concert.Genre))
            {
                if (s == FilmHandler.GetListOfGenres(concert.Genre).Last())
                {
                    this.GenreConcert.Text += s;
                    break;
                }
                this.GenreConcert.Text += s + ", ";
            }
            if (concert.Address != null)
            {
                if (!concert.Address.Equals(""))
                {
                    if (concert.Address[0] == ' ')
                    {
                        concert.Address = concert.Address.Remove(0, 1);
                    }
                }
                this.PlaceCocert.Text = concert.Address + "\n";
            }
            if (concert.Event_Date != null && !concert.Event_Date.Equals(""))
            {
                this.TimetableConcert.Text = concert.Event_Date;
            }
            this.concert = concert;
        }

        private void DetailsConcert_Click(object sender, RoutedEventArgs e)
        {
            this.win.concert = this.concert;
        }

        private void AddToFavoriteConcert_Click(object sender, RoutedEventArgs e)
        {
            AddFavOnConcert.Visibility = Visibility.Collapsed;
            AddFavOffConcert.Visibility = Visibility.Visible;
            flagFavorite = true;
        }

        private void DeleteToFavoriteConcert_Click(object sender, RoutedEventArgs e)
        {
            AddFavOnConcert.Visibility = Visibility.Visible;
            AddFavOffConcert.Visibility = Visibility.Collapsed;
            flagFavorite = false;
        }
    }
}
