using BookCathalog.Dal.Models;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookCathalog.Views
{
    public partial class BookView : UserControl
    {
        public BookView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty BookProperty =
            DependencyProperty.Register("Book", typeof(Book), typeof(BookView));

        public Book Book
        {
            get => (Book)GetValue(BookProperty);
            set => SetValue(BookProperty, value);
        }
        private void PreviewYearInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Bitmap picture";
            dialog.DefaultExt = ".bmp";
            dialog.Filter = "Bitmap pictures (.bmp)|*.bmp";
            bool? result = dialog.ShowDialog();
            if (result != null && result == true)
            {
                Book.FrontPage = File.ReadAllBytes(dialog.FileName);
            }

        }
    }
}
