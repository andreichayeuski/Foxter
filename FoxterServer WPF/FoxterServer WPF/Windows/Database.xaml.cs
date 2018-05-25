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

namespace FoxterServer_WPF
{
    public enum Tables { Films, Cinemas, Sessions, Users };

    public partial class Database : Window
    {
        public Tables Tables { get; set; }

        public FilmContext filmContext = new FilmContext();

        public Database()
        {
            InitializeComponent();
            OutputFunc();
        }

        private void Insert_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (this.Tables)
                {
                    case Tables.Films:
                        {
                            
                            break;
                        }
                    case Tables.Cinemas:
                        {

                            break;
                        }
                    case Tables.Sessions:
                        {

                            break;
                        }
                    case Tables.Users:
                        {

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void OutputFunc()
        {
            try
            {
                var output = from b in this.filmContext.Films
                             select b;
                this.FilmGrid.DataContext = output.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UserGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Tables = Tables.Users;
                this.UserGrid.ItemsSource = this.filmContext.Users.ToList<User>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void FilmGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Tables = Tables.Films;
                this.FilmGrid.ItemsSource = this.filmContext.Films.ToList<Film>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void CinemaGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Tables = Tables.Cinemas;
                this.CinemaGrid.ItemsSource = this.filmContext.Cinemas.ToList<Cinema>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void SessionGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Tables = Tables.Sessions;
                this.SessionGrid.ItemsSource = this.filmContext.Sessions.ToList<Session>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("save");
        }

        /*public void Updfunc()
        {
            try
            {
                Book _id = (Book)this.Output.SelectedValue;
                int www = _id.Id;

                var output = (from c in this.db.MyBooks
                              where c.Id == www
                              select c).Single<Book>();
                string UserAnswer = Interaction.InputBox("Введите название поля и измененный вариант поля", "", "");
                string[] type = UserAnswer.Split(' ');
                switch (type[0])
                {
                    case "Author":
                    case "Автор":
                        {
                            output.Author = type[1];
                            break;
                        }
                    case "Title":
                    case "Название":
                        {
                            output.Title = type[1];
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("Неверный ввод названия столбца ");
                            break;
                        }
                };
                this.db.SaveChanges();
                Outputfunc();
            }
            catch (Exception ex)
            {
                // MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void Dltfunc()
        {
            try
            {
                string UserAnswer = Interaction.InputBox("Введите Id поля, которое хотите удалить или ALL, если хотите удалить все.", "", "");
                switch (Int32.TryParse(UserAnswer, out int dltid))
                {
                    case true:
                        var w = (from d in this.db.MyBooks
                                 where d.Id == dltid
                                 select d).ToList<Book>();
                        this.db.MyBooks.Remove(w.First());
                        this.db.SaveChanges();
                        break;

                    case false:
                        switch (UserAnswer)
                        {
                            case "All":

                                var w_ = (from d in this.db.MyBooks
                                          select d).ToList<Book>();
                                foreach (Book books in w_)
                                {
                                    this.db.MyBooks.Remove(books);
                                    this.db.SaveChanges();
                                }
                                break;
                            default: break;
                        }
                        break;
                }
                Outputfunc();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error: " + ex.Message);
            }
        }*/
    }
}
