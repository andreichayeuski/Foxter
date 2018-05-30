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
using System.Text.RegularExpressions;
using ClassLibrary;

namespace CP_WPF.View
{
    /// <summary>
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    /// 

    public partial class Search : UserControl
    {
        MainMenuxaml win;
        MainWindow mwin;
        public Film film;
        //WrapPanel grid;
        public bool flag = false;
        public Search(MainMenuxaml win)
        {
            this.win = win;
            InitializeComponent();
        }

        public bool SearchMach(TextBox text)
        {
            return true;
        }
        
        private void Serching(object sender, RoutedEventArgs e)
        {
            try
            {
                Regex regex = new Regex(@"[А-Я][а-я\ ]+", RegexOptions.IgnoreCase);
                MatchCollection matches = regex.Matches(SearchQuery.Text);
                if (matches.Count < 0)
                {
                    win.GridSpaceInfo.Children.Add(new NotFound(win));
                }
                if (matches.Count > 0 && SearchQuery.Text.Length != 0)
                {
                    foreach (Match match in matches)
                    {
                        List<CardItem> list = new List<CardItem>(AsyncClient.films.Count);
                        foreach (Film t in AsyncClient.films)
                        {
                            if (t.Name.Contains(SearchQuery.Text))
                            {
                                win.GridSpaceInfo.Children.Clear();
                                CardItem cardItem = new CardItem(mwin,win, t);
                                win.GridSpaceInfo.Children.Add(new CardItem(mwin,win, t));
                                cardItem.Details.Click += new RoutedEventHandler(Handler);
                            }
                        }
                    }
                }
                else
                {
                    win.GridSpaceInfo.Children.Clear();
                    SearchQuery.Text = "Wrong symbols";
                    win.GridSpaceInfo.Children.Add(new Search(win));
                    win.GridSpaceInfo.Children.Add(new NotFound(win));
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void OnSearchWords(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!SearchQuery.Text.Equals(string.Empty))
                {
                    var query = from t in AsyncClient.films
                                where t.Name.ToLower().Contains(SearchQuery.Text) || t.Name.Contains(SearchQuery.Text) || t.Name.ToUpper().Contains(SearchQuery.Text)
                                orderby t.Name
                                select t;

                    if (query.Count() > 0)
                    {
                        if (flag)
                        {
                            win.GridSpaceInfo.Children.RemoveRange(1, win.GridSpaceInfo.Children.Count);
                        }
                        else
                        {
                            win.GridSpaceInfo.Children.RemoveRange(1, win.GridSpaceInfo.Children.Count - 1);
                        }
                        foreach (Film t in query)
                        {
                            CardItem cardItem = new CardItem(mwin,win, t);
                            win.GridSpaceInfo.Children.Add(new CardItem(mwin,win, t));
                            cardItem.Details.Click += new RoutedEventHandler(Handler);
                            flag = true;
                        }
                    }
                    else if (query.Count() <= 0)
                    {
                        win.GridSpaceInfo.Children.RemoveRange(1, win.GridSpaceInfo.Children.Count - 1);
                        win.GridSpaceInfo.Children.Add(new NotFound(win));
                        flag = false;
                    }
                }
                else
                {
                    win.GridSpaceInfo.Children.RemoveRange(1, win.GridSpaceInfo.Children.Count);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.Source + ex.Data);
            }

        }

        public void Handler(object sender, RoutedEventArgs e)
        {
            win.GridSpaceInfo.Children.Clear();
            win.GridSpaceInfo.Children.Add(new CardItemMoreInfo(win, film));
        }
    }
}

