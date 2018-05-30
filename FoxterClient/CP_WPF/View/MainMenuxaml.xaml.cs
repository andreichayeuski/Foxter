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
using System.Windows.Shapes;
using ClassLibrary;
namespace CP_WPF.View
{
    public partial class MainMenuxaml : Window
    {
        WrapPanel grid;
        public Film film;
        public Concert concert;
        public Exhibition exhibition;
        public Cinema cinema;
        public Comment comment = new Comment();
        MainWindow win;

        CardItem card;
        public User user;
        public bool flagForumOpen = false;
        public MainMenuxaml(MainWindow win)
        {
            InitializeComponent();
            this.win = win;
            for (int i = 1; i < 8; i++)
            {
                AsyncClient.SetTypeInfo((TypeOfInfo)i);
                AsyncClient.StartClient();
            }
            List<CardItem> list = new List<CardItem>(AsyncClient.films.Count);

            foreach (Film a in AsyncClient.films)
            {
                CardItem cardItem = new CardItem(win,this, a);
                cardItem.Details.Click += new RoutedEventHandler(HandlerFilm);
                list.Add(cardItem);
                GridSpaceInfo.Children.Add(cardItem);
                grid = GridSpaceInfo;
            }
            //List<Comment> listcomments = new List<Comment>(AsyncClient.comment.Id);

        }

        public bool flagH = false;
        public bool flagS = false;

        public void HandlerFilm(object sender, RoutedEventArgs e)
        {
            GridSpaceInfo.Children.Clear();
            GridSpaceInfo.Children.Add(new CardItemMoreInfo(this, film));
        }

        public void HandlerCinema(object sender, RoutedEventArgs e)
        {
            GridSpaceInfo.Children.Clear();
            GridSpaceInfo.Children.Add(new CardItemMoreInfoCinema(this, cinema));
        }

        private void ButtonMinimaze_Click(object sender, RoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application application = Application.Current;
            application.Shutdown();
        }

        private void ButtonMultiTabUp_Click(object sender, RoutedEventArgs e)
        {
            ButtonMultiTabUp.Visibility = Visibility.Collapsed;
            ButtonMultiTabDown.Visibility = Visibility.Visible;
            SystemCommands.MaximizeWindow(this);
            GridSpaceInfo.Margin = new Thickness(80, 0, 0, 0);
            flagS = true;

        }
        private void ButtonMultiTabDown_Click(object sender, RoutedEventArgs e)
        {
            ButtonMultiTabUp.Visibility = Visibility.Visible;
            ButtonMultiTabDown.Visibility = Visibility.Collapsed;
            SystemCommands.RestoreWindow(this);
            flagS = false;
        }

        private void ButtonMenuOpen_Click(object sender, RoutedEventArgs e)
        {
            MainBtnToolbarOpen.Visibility = Visibility.Collapsed;
            MainBtnToolbarClose.Visibility = Visibility.Visible;
            ToolbarPanel.Margin = new Thickness(0);

            if (flagH == true)
            {
                GridSpaceInfo.Margin = new Thickness(230, 60, 0, 0);
            }
            else
            {
                GridSpaceInfo.Margin = new Thickness(230, 0, 0, 0);
            }
        }

        private void ButtonMenuClose_Click(object sender, RoutedEventArgs e)
        {
            MainBtnToolbarOpen.Visibility = Visibility.Visible;
            MainBtnToolbarClose.Visibility = Visibility.Collapsed;
            ToolbarPanel.Margin = new Thickness(-20, 0, 520, 0);

            if (flagH == true)
            {
                GridSpaceInfo.Margin = new Thickness(0, 60, 0, 0);
            }
            else
            {
                GridSpaceInfo.Margin = new Thickness(0);
            }
        }

        private void ButtonLogInOpen_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void ButtonPanelOpen_Click(object sender, RoutedEventArgs e)
        {
            MainNavSencondPart.Visibility = Visibility.Visible;
            MainBtnDropDown.Visibility = Visibility.Collapsed;
            MainBtnDropUp.Visibility = Visibility.Visible;
            GridSpaceInfo.Margin = new Thickness(0, 60, 0, 0);
            flagH = true;
        }

        private void ButtonPanelClose_Click(object sender, RoutedEventArgs e)
        {
            MainNavSencondPart.Visibility = Visibility.Collapsed;
            MainBtnDropUp.Visibility = Visibility.Collapsed;
            MainBtnDropDown.Visibility = Visibility.Visible;
            GridSpaceInfo.Margin = new Thickness(0);
            flagH = false;
        }

        private void Cinema_Click(object sender, RoutedEventArgs e)
        {
            GridSpaceInfo.Children.Clear();
            flagForumOpen = false;
            List<CardItemCinema> list = new List<CardItemCinema>(AsyncClient.cinemas.Count);
            foreach (Cinema a in AsyncClient.cinemas)
            {
                CardItemCinema cardItemCinema = new CardItemCinema(this, a);
                cardItemCinema.Details.Click += new RoutedEventHandler(HandlerCinema);
                list.Add(cardItemCinema);
                GridSpaceInfo.Children.Add(cardItemCinema);
                grid = GridSpaceInfo;
            }
        }

        private void MainBtnFilms_Click(object sender, RoutedEventArgs e)
        {
            InfoScrollWrapper.Visibility = Visibility.Visible;
            CommentReviewScrollWrapper.Visibility = Visibility.Collapsed;

            GridSpaceInfo.Children.Clear();
            flagForumOpen = false;
            List<CardItem> list = new List<CardItem>(AsyncClient.films.Count);
            foreach (Film a in AsyncClient.films)
            {
                CardItem cardItem = new CardItem(win,this, a);
                cardItem.Details.Click += new RoutedEventHandler(HandlerFilm);
                list.Add(cardItem);
                GridSpaceInfo.Children.Add(cardItem);
                grid = GridSpaceInfo;
                if (cardItem.flagFavorite)
                {
                    cardItem.AddFavOn.Visibility = Visibility.Collapsed;
                    cardItem.AddFavOff.Visibility = Visibility.Visible;
                }
                else
                {
                    cardItem.AddFavOn.Visibility = Visibility.Visible;
                    cardItem.AddFavOff.Visibility = Visibility.Collapsed;
                }

            }
        }

        private void MainBtnConcert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InfoScrollWrapper.Visibility = Visibility.Visible;
                CommentReviewScrollWrapper.Visibility = Visibility.Collapsed;

                GridSpaceInfo.Children.Clear();
                flagForumOpen = false;
                List<CardItemConcert> list = new List<CardItemConcert>(AsyncClient.concerts.Count);
                foreach (Concert con in AsyncClient.concerts)
                {
                    CardItemConcert cardItem = new CardItemConcert(this, con);
                    cardItem.Details.Click += new RoutedEventHandler(HandlerFilm);
                    list.Add(cardItem);
                    GridSpaceInfo.Children.Add(cardItem);
                    grid = GridSpaceInfo;
                    if (cardItem.flagFavorite)
                    {
                        cardItem.AddFavOnConcert.Visibility = Visibility.Collapsed;
                        cardItem.AddFavOffConcert.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        cardItem.AddFavOnConcert.Visibility = Visibility.Visible;
                        cardItem.AddFavOffConcert.Visibility = Visibility.Collapsed;
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void MainBtnExhibition_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InfoScrollWrapper.Visibility = Visibility.Visible;
                CommentReviewScrollWrapper.Visibility = Visibility.Collapsed;

                GridSpaceInfo.Children.Clear();
                flagForumOpen = false;
                List<CardItemExhibition> list = new List<CardItemExhibition>(AsyncClient.exhibitions.Count);
                foreach (Exhibition exh in AsyncClient.exhibitions)
                {
                    CardItemExhibition cardItem = new CardItemExhibition(this, exh);
                    cardItem.Details.Click += new RoutedEventHandler(HandlerFilm);
                    list.Add(cardItem);
                    GridSpaceInfo.Children.Add(cardItem);
                    grid = GridSpaceInfo;
                    if (cardItem.flagFavorite)
                    {
                        cardItem.AddFavOnExhibition.Visibility = Visibility.Collapsed;
                        cardItem.AddFavOffExhibition.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        cardItem.AddFavOnExhibition.Visibility = Visibility.Visible;
                        cardItem.AddFavOffExhibition.Visibility = Visibility.Collapsed;
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void MainBtnOther_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenSearch(object sender, RoutedEventArgs e)
        {
            GridSpaceInfo.Children.Clear();
            Search search = new Search(this);
            flagForumOpen = false;
            GridSpaceInfo.Children.Add(new Search(this));
            if (flagS == true)
            {
                search.Margin = new Thickness(10, 10, 10, 0);

            }
            else
            {
                search.Margin = new Thickness(0);
            }

        }

        private void OpenFavorite(object sender, RoutedEventArgs e)
        {
            flagForumOpen = false;
            if (win.flagautoriz)
            {
                GridSpaceInfo.Children.Clear();
                if (card.flagFavorite)
                {
                    card = new CardItem(win, this, film);
                }
            }
            else
            {
                MessageBox.Show("Sorry, please to register");
            }
        }

        private void OpenComments(object sender, RoutedEventArgs e)
        {
            try
            {
                GridSpaceInfo.Children.Clear();
                flagForumOpen = true;
                InfoScrollWrapper.Visibility = Visibility.Collapsed;
                CommentReviewScrollWrapper.Visibility = Visibility.Visible;                
                CommentReviewCreator commentReviewCreator = new CommentReviewCreator(this);
                if (win.flagautoriz)
                {
                    CommentReviewCreatorGridWrapper.Children.Add(new CommentReviewCreator(this));
                    foreach (Comment c in AsyncClient.comments)
                    {
                        CommentReviewViewGridWrapper.Children.Add(new CommentReviewView(this, c));
                    }
                }
                else
                {
                    MessageBox.Show("Now you are in Guest mode, for to write comments, please register!");

                    if (comment.Text == "" || comment.Text == " ")
                    {
                        CommentReviewViewGridWrapper.Children.Add(new CommentReviewView(this, comment));
                    }
                    else
                    {
                        MessageBox.Show("Sorry, so far ther isn't a single comment.");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Трабл в майне");
            }


        }

        private void ButtonEnglish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Resources = new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/Resourse/Dictionary/En_en.xaml")
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }

        private void ButtonRussian_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Resources = new ResourceDictionary()
                {
                    Source = new Uri("pack://application:,,,/Resourse/Dictionary/Ru_ru.xaml")

                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.Message);
            }
        }
    }
}
