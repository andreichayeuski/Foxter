using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data.Entity.Validation;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using ClassLibrary;

namespace CP_WPF.Model
{
    public static class Validation
    {       

        public static bool NoDuplicateUserName(string UserNameValue)
        {
            bool isNoRepeat = true;                 
            try
            {
                // Getting object from database and outputting in MessegeBox

                List<User> users = new List<User>();                
                foreach (User u in users)
                {                    
                    if (UserNameValue.ToLower()==u.UserName.ToLower())
                    {
                        isNoRepeat = false;
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    MessageBox.Show(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        MessageBox.Show(String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            return isNoRepeat;
        }

        public static string GetHashString(string s)
        {
            //переводим строку в байт-массив  
            byte[] bytes = Encoding.Unicode.GetBytes(s);
 
            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =  new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
    public partial class MainWindow : Window
    {
        public static bool CheckUserName(TextBox Username, Image image)
        {
            string regex = @"[0-9a-zA-Z]";
            bool valid = true;
            ToolTip toolTip = new ToolTip();

            for (int i = 0; i < Username.Text.Length; i++)
            {
                if (Regex.IsMatch(Username.Text[i].ToString(), regex) == false)
                {
                    valid = false;
                    toolTip.Content = "Wrong symbol";
                }
            }

            if (Username.Text.Length == 0)
            {
                valid = false;
                toolTip.Content = "Can not be empty";
            }

            if (Username.Name == "NewLogin")
            {
                if (Validation.NoDuplicateUserName(Username.Text) == false)
                {
                    valid = false;
                    toolTip.Content = "This login exist yet.";
                }
            }

            if (valid == false)
            {
                Username.BorderBrush = new SolidColorBrush(Colors.DarkMagenta);
                image.Visibility = Visibility.Visible;
                image.ToolTip = toolTip;
            }

            return valid;
        }
        
        public static  bool CheckUserPassword(PasswordBox passwordBox, Image image)
        {
            bool valid = true;
            string regex = @"[0-9a-zA-Z]";
            ToolTip toolTip = new ToolTip();

            for (int i = 0; i < passwordBox.Password.Length; i++)
            {
                if (Regex.IsMatch(passwordBox.Password[i].ToString(), regex) == false)
                {
                    valid = false;
                    toolTip.Content = "Wrong symbol";
                }
            }

            if (passwordBox.Password.Length == 0)
            {
                valid = false;
                toolTip.Content = "Can not be empty";
            }

           
            if (valid == false)
            {
                passwordBox.BorderBrush = new SolidColorBrush(Colors.DarkMagenta);
                image.Visibility = Visibility.Visible;
                image.ToolTip = toolTip;
            }

            return valid;
        }
    }
}
