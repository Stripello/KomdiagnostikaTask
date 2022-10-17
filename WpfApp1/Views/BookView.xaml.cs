using BookCathalog.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace BookCathalog.Views
{
    public partial class BookView : UserControl
    {
        public BookView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty BookProperty = 
            DependencyProperty.Register("Book", typeof(Book),typeof(BookView));

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
    }
}
