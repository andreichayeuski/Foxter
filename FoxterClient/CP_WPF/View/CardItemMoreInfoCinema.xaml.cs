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
    public partial class CardItemMoreInfoCinema : UserControl
    {
        Cinema cinema;
        MainMenuxaml win;

        public CardItemMoreInfoCinema(MainMenuxaml win, Cinema cinema)
        {
            //this.win = win;
            //InitializeComponent();

            //Uri urislide1 = new Uri(film.Image_Main, UriKind.RelativeOrAbsolute);
            //this.CardInfoMainImg.Source = BitmapFrame.Create(urislide1);

            //if (film.Images != "")
            //{
            //    foreach (string s in FilmHandler.GetListOfImages(film.Images))
            //    {
            //        Uri urislide = new Uri(s, UriKind.RelativeOrAbsolute);
            //        Image image = new Image
            //        {

            //            Source = BitmapFrame.Create(urislide),
            //            Margin = new Thickness(0, 3, 7, 3),
            //            Height = 200,
            //            Focusable = true

            //        };
            //        this.Images.Children.Add(image);
            //    }
            //}
            //this.EventNameCardMoreInfo.Text = film.Name;
            //List<string> list = FilmHandler.GetListOfGenres(film.Genre);
            //foreach (string s in list)
            //{
            //    if (s == list.Last())
            //    {
            //        this.GenresCardMoreInfo.Text += s;
            //        break;
            //    }
            //    this.GenresCardMoreInfo.Text += s + ", ";
            //}
            //if (film.Country != null)
            //{
            //    if (!film.Country.Equals(""))
            //    {
            //        if (film.Country[0] == ' ')
            //        {
            //            film.Country = film.Country.Remove(0, 1);
            //        }
            //    }
            //    this.YearCardMoreInfo.Text = film.Year;
            //}
            //this.CountryCardMoreInfo.Text = film.Country;
            //this.DescriptionCardMoreInfo.Text = film.Info;
            //this.BoxOfficeCardMoreInfo.Text = film.Date;
            //this.RatingCardMoreInfo.Text = film.Rating;
            //this.TimeCardMoreInfo.Text = film.Duration;
        }
    }
}
