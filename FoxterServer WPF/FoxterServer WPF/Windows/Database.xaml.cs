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
using System.Windows.Controls.Primitives;
using ClassLibrary;

namespace FoxterServer_WPF
{
    public enum All_Table { Films, Cinemas, Sessions, Users };

    public class Tables
    {
        public All_Table Table { get; set; }
        public char Code { get; set; }
        public bool IsChecked { get; set; }

        public static List<Tables> GetListOfTables()
        {
            List<Tables> ListTables = new List<Tables>();
            for (int i = 0; i < 4; i++)
            {
                Tables tables = new Tables
                {
                    Table = (All_Table)i
                };
                tables.Code = tables.Table.ToString()[0];
                ListTables.Add(tables);
            }
            return ListTables;
        }
    }

    public partial class Database : Window
    {
        public All_Table Table;

        public FoxterContext foxterContext = new FoxterContext();

        public Database()
        {
            InitializeComponent();
            this.TypeListBox.ItemsSource = Tables.GetListOfTables();
        }

        #region Methods %Grid_Loaded

        private void UserGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Table = All_Table.Users;
                this.UserGrid.ItemsSource = this.foxterContext.Users.ToList<User>();
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
                this.Table = All_Table.Films;
                this.FilmGrid.ItemsSource = this.foxterContext.Films.ToList<Film>();
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
                this.Table = All_Table.Cinemas;
                this.CinemaGrid.ItemsSource = this.foxterContext.Cinemas.ToList<Cinema>();
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
                this.Table = All_Table.Sessions;
                this.SessionGrid.ItemsSource = this.foxterContext.Sessions.ToList<Session>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        #endregion

        #region Save, Update, Insert, Delete
        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.foxterContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SaveError: " + ex.Message);
            }
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (Tables t in this.TypeListBox.ItemsSource)
                {
                    if (t.IsChecked)
                    {
                        switch (t.Table)
                        {
                            case All_Table.Films:
                                {
                                    List<Film> films = FilmContext.GetListOfFilms();
                                    this.foxterContext.CheckAndUpdateFilmInTable(films);
                                    break;
                                }
                            case All_Table.Cinemas:
                                {
                                    List<Cinema> cinemas = CinemaContext.GetListOfCinemas();
                                    this.foxterContext.CheckAndUpdateCinemaInTable(cinemas);
                                    break;
                                }
                            case All_Table.Sessions:
                                {
                                    List<SessionFromFile> sessions_from_file = SessionContext.GetListOfSessions();
                                    this.foxterContext.MakeListOfSessionAndAddToDatabase(sessions_from_file);
                                    break;
                                }
                            case All_Table.Users:
                                {

                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update error: " + ex.Message);
            }
        }

        #region Methods for Update
        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            foreach (Tables t in this.TypeListBox.Items)
            {
                if ((char)toggleButton.Content == t.Code)
                {
                    t.IsChecked = true;
                }
            }
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            foreach (Tables t in this.TypeListBox.Items)
            {
                if ((char)toggleButton.Content == t.Code)
                {
                    t.IsChecked = false;
                }
            }
        }
        #endregion

        private void Insert_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (this.Table)
                {
                    case All_Table.Films:
                        {
                            if (this.FilmGrid.SelectedIndex != -1)
                            {
                                foreach (Film f in this.FilmGrid.SelectedItems)
                                {
                                    this.foxterContext.Films.Add(f);
                                }
                            }
                            break;
                        }
                    case All_Table.Cinemas:
                        {
                            if (this.CinemaGrid.SelectedIndex != -1)
                            {
                                foreach (Cinema c in this.CinemaGrid.SelectedItems)
                                {
                                    this.foxterContext.Cinemas.Add(c);
                                }
                            }
                            break;
                        }
                    case All_Table.Sessions:
                        {
                            if (this.SessionGrid.SelectedIndex != -1)
                            {
                                foreach (Session s in this.SessionGrid.SelectedItems)
                                {
                                    this.foxterContext.Sessions.Add(s);
                                }
                            }
                            break;
                        }
                    case All_Table.Users:
                        {
                            if (this.UserGrid.SelectedIndex != -1)
                            {
                                foreach (User u in this.UserGrid.SelectedItems)
                                {
                                    this.foxterContext.Users.Add(u);
                                }
                            }
                            break;
                        }
                }
                this.foxterContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (this.Table)
                {
                    case All_Table.Films:
                        {
                            if (this.FilmGrid.SelectedIndex != -1)
                            {
                                foreach (Film f in this.FilmGrid.SelectedItems)
                                {
                                    this.foxterContext.Films.Remove(f);
                                }
                            }
                            break;
                        }
                    case All_Table.Cinemas:
                        {
                            if (this.CinemaGrid.SelectedIndex != -1)
                            {
                                foreach (Cinema c in this.CinemaGrid.SelectedItems)
                                {
                                    this.foxterContext.Cinemas.Remove(c);
                                }
                            }
                            break;
                        }
                    case All_Table.Sessions:
                        {
                            if (this.SessionGrid.SelectedIndex != -1)
                            {
                                foreach (Session s in this.SessionGrid.SelectedItems)
                                {
                                    this.foxterContext.Sessions.Remove(s);
                                }
                            }
                            break;
                        }
                    case All_Table.Users:
                        {
                            if (this.UserGrid.SelectedIndex != -1)
                            {
                                foreach (User u in this.UserGrid.SelectedItems)
                                {
                                    this.foxterContext.Users.Remove(u);
                                }
                            }
                            break;
                        }
                }
                this.foxterContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete error: " + ex.Message);
            }
        }
#endregion
    }
}
