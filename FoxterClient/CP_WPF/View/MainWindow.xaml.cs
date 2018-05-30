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
using System.Diagnostics;

namespace CP_WPF.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        //readonly IDisposable disposable;
        public bool flagautoriz = false;

        public MainWindow()
        {
            InitializeComponent();

            AsyncClient.SetTypeInfo(TypeOfInfo.Users);
            AsyncClient.StartClient();

            MainWindowRight.Children.Add(new Logo(this));
            MainWindowCP.Children.Add(new LogInControl(this));
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

        private void ButtonCRussian_Click(object sender, RoutedEventArgs e)
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
    }
}
