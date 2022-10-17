﻿using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookCathalog.Views
{
    public partial class AddBookDialog : UserControl
    {
        public AddBookDialog()
        {
            InitializeComponent();
        }

        private void PreviewYearInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
