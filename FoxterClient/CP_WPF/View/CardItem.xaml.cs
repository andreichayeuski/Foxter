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
using System.Drawing;
using System.Windows.Shapes;
using ClassLibrary;

namespace CP_WPF.View
{
    public partial class CardItem : UserControl
    {
        public Film film;
        MainMenuxaml win;
        MainWindow mainWindow;
        public bool flagFavorite = false;

        public CardItem(MainWindow mwin, MainMenuxaml win, Film film)
        {
            this.mainWindow = mwin;
            this.win = win;
            InitializeComponent();
            Uri urislide1 = new Uri(film.Image_Main, UriKind.RelativeOrAbsolute);
            this.MainImage.Source = BitmapFrame.Create(urislide1);
            this.EventName.Text = film.Name;
            if (!film.Genre.Equals(""))
            {
                foreach (string s in FilmHandler.GetListOfGenres(film.Genre))
                {
                    if (s == FilmHandler.GetListOfGenres(film.Genre).Last())
                    {
                        this.Genre.Text += s;
                        break;
                    }
                    this.Genre.Text += s + ", ";
                }
            }
            else
            {
                this.Genre.Visibility = Visibility.Collapsed;
            }

            if (!film.Country.Equals(""))
            {
                if (film.Country[0] == ' ')
                {
                    film.Country = film.Country.Remove(0, 1);
                }
                this.CountryAndYear.Text = film.Country + " " + film.Year;
            }
            else
            {
                this.CountryAndYear.Visibility = Visibility.Collapsed;
            }            

            if (!film.Date.Equals(""))
            {
                this.BoxOffice.Text = film.Date;
            }
            else
            {
                this.BoxOffice.Visibility = Visibility.Collapsed;
            }
            this.film = film;
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            this.win.film = this.film;
        }
        
        private void AddToFavorite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mainWindow.flagautoriz)
                {
                    AddFavOn.Visibility = Visibility.Collapsed;
                    AddFavOff.Visibility = Visibility.Visible;
                    flagFavorite = true;
                }
                else
                {
                    MessageBox.Show("To add an Event to your favorites? please log in or register!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }         

        }

        private void DeleteToFavorite_Click(object sender, RoutedEventArgs e)
        {
            AddFavOn.Visibility = Visibility.Visible;
            AddFavOff.Visibility = Visibility.Collapsed;
            flagFavorite = false;
        }
    }
}
