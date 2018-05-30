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
    /// Логика взаимодействия для CommentReviewView.xaml
    /// </summary>
    public partial class CommentReviewView : UserControl
    {
        MainMenuxaml win;
        public Comment comment;

        public CommentReviewView(MainMenuxaml win, Comment comment)
        {
            this.win = win;
            InitializeComponent();
            //TextRange range = new TextRange(this.RichTextComment.Document.ContentStart, this.RichTextComment.Document.ContentEnd);
            if (comment.UserName.Length > 0 && comment.Text.Length > 0)
            {
                AuthorNameView.Text = comment.UserName;
                ThemeView.Text = comment.Theme;
                PlaceView.Text = comment.Place;
                TimeView.Text = comment.Date;
                TextComment.Text = comment.Text;
            }
            this.comment = comment;
        }
    }
}
