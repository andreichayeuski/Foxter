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
    /// Логика взаимодействия для CommentReviewCreator.xaml
    /// </summary>
    
    public partial class CommentReviewCreator : UserControl
    {
        MainMenuxaml win;
        public CommentReviewCreator(MainMenuxaml win)
        {
            this.win = win;
            InitializeComponent();
            AuthorName.Text = (string)win.UserNameMain.Content;
        }

        private void ToSendComment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AsyncClient.SetTypeInfo(TypeOfInfo.Comment);
                AsyncClient.StartClient();
                Comment comment = new Comment
                {
                    Id = 1,
                    UserName = AuthorName.Text,
                    Text = TextComment.Text,
                    Date = Convert.ToString(System.DateTime.Today),
                    Theme = Convert.ToString(TheamesCreator.SelectedValue),
                    Place = Convert.ToString(PlaceCreator.SelectedItem)
                };
                if (TheamesCreator.SelectedItem == null)
                {
                    MessageBox.Show("Please select theme to which your comment applies!");
                    TheamesCreator.BorderBrush = Brushes.Pink;
                    TheamesCreator.BorderThickness = new Thickness(2);
                }
                if (PlaceCreator.SelectedItem == null)
                {
                    MessageBox.Show("Please select the place, that you visited for your comment!");
                    PlaceCreator.BorderThickness = new Thickness(2);
                    PlaceCreator.BorderBrush = Brushes.Pink;
                }
                AsyncClient.SetComment(comment);
                AsyncClient.SetTypeInfo(TypeOfInfo.Comment);
                AsyncClient.StartClient();
                AsyncClient.SetTypeInfo(TypeOfInfo.Comments);
                AsyncClient.StartClient();
                AsyncClient.comments.Add(comment);                     
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Хз в чём трабл");
            }
        }

        private void ToClearText_Click(object sender, RoutedEventArgs e)
        {
            TextComment.Clear();
        }
    }
}
