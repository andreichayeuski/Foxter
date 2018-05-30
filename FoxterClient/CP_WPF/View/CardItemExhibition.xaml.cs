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
    /// Логика взаимодействия для CardItemExebition.xaml
    /// </summary>
    public partial class CardItemExhibition : UserControl
    {
        public Exhibition exhibition;
        MainMenuxaml win;
        public bool flagFavorite = false;

        public CardItemExhibition(MainMenuxaml win, Exhibition exhibition)
        {
            this.win = win;
            InitializeComponent();
            Uri urislide1 = new Uri(exhibition.Image_Main, UriKind.RelativeOrAbsolute);
            this.MainImageExhibition.Source = BitmapFrame.Create(urislide1);
            this.EventNameExhibition.Text = exhibition.Name ?? "";
            
            if (exhibition.Address != null)
            {
                if (!exhibition.Address.Equals(""))
                {
                    if (exhibition.Address[0] == ' ')
                    {
                        exhibition.Address = exhibition.Address.Remove(0, 1);
                    }
                }
                this.PlaceExhibition.Text = exhibition.Address + "\n";
            }
            if (exhibition.Event_Date != null && !exhibition.Event_Date.Equals(""))
            {
                this.TimetableExhibition.Text = exhibition.Event_Date;
            }            
            this.exhibition = exhibition;
        }

        private void DetailsExhibition_Click(object sender, RoutedEventArgs e)
        {
            this.win.exhibition = this.exhibition;
        }

        private void AddToFavoriteExhibition_Click(object sender, RoutedEventArgs e)
        {
            AddFavOnExhibition.Visibility = Visibility.Collapsed;
            AddFavOffExhibition.Visibility = Visibility.Visible;
            flagFavorite = true;

        }

        private void DeleteToFavoriteExhibition_Click(object sender, RoutedEventArgs e)
        {
            AddFavOnExhibition.Visibility = Visibility.Visible;
            AddFavOffExhibition.Visibility = Visibility.Collapsed;
            flagFavorite = false;
        }
    }
}
