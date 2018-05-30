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
    /// Логика взаимодействия для LogInControl.xaml
    /// </summary>
    public partial class LogInControl : UserControl
    {
        MainWindow win;
        MainMenuxaml mainMenuxaml;
        
        //SignUpControl signUpControl = new SignUpControl(win);

        public LogInControl(MainWindow win)
        {
            this.win = win;           
            InitializeComponent();
        }

        private void ArrowSignUp(object sender, RoutedEventArgs e)
        {
            win.MainWindowCP.Children.Clear();
            win.MainWindowCP.Children.Add(new SignUpControl(win));
        }

        
       

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isLoginCorrect = Model.MainWindow.CheckUserName(Username, Alert_Login);
            bool isPasswordCorrect = Model.MainWindow.CheckUserPassword(Password, Alert_Password);
            //List<User> users = new List<User>();
            this.mainMenuxaml = new MainMenuxaml(this.win);
            if (isLoginCorrect && isPasswordCorrect)
            {
                try
                {
                    User user = new User
                    {
                        UserName = Username.Text,
                        Password = Model.Validation.GetHashString(Password.Password)
                    };
                    Model.CurrentUser.user = user;                 
                    
                    bool isUserFind = false;

                    foreach (User u in AsyncClient.users)
                    {
                        if (user.Password == u.Password && user.UserName == u.UserName)
                        {
                            isUserFind = true;                            
                            mainMenuxaml.UserNameMain.Content = user.UserName;         
                            AsyncClient.SetUser(user);
                            win.flagautoriz = true;
                            win.Close();
                            mainMenuxaml.Show();                               
                        }

                    }
                    if (isUserFind == false)
                    {
                        MessageBox.Show("Проверьте корректность введённых данных");
                    }                  

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
    }
}
