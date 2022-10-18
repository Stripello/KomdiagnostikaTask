using BookCathalog.Dal.Models;
using BookCathalog.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BookCathalog.ViewModels
{
    public class AddBookDialogViewModel : BindableBase, IDialogAware
    {
        public AddBookDialogViewModel(IbookServise bookService, IImageProcessor imageProcessor)
        {
            _bookServise = bookService;
            _imageProcessor = imageProcessor;
        }
        private IImageProcessor _imageProcessor;
        private IbookServise _bookServise;
        public Book CurrentBook { get; set; } = new Book();
        private string _frontPageLocation = string.Empty;
        public string FrontPageLocation
        {
            get => _frontPageLocation;
            set => SetProperty<string>(ref _frontPageLocation, value);
        }

        private DelegateCommand _addBookCommand;
        public DelegateCommand AddBookCommand =>
            _addBookCommand ?? (_addBookCommand = new DelegateCommand(AddBook));

        private void AddBook()
        {
            _bookServise.AddBook(CurrentBook);
        }
        
        private DelegateCommand _selectFrontPage;
        public DelegateCommand SelectFrontPageCommand =>
            _selectFrontPage ?? (_selectFrontPage = new DelegateCommand(SelectFrontPage));
        
        private void SelectFrontPage()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Bitmap picture";
            dialog.DefaultExt = ".bmp";
            dialog.Filter = "Bitmap pictures (.bmp)|*.bmp";
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                FrontPageLocation = dialog.FileName;
            }
        }
        
        //Some neceserry Prism stuff.
        public string Title { get; set; } = "Adding book";

        public event Action<IDialogResult> RequestClose;
        public bool CanCloseDialog() { return true; }
        public void OnDialogClosed() { }
        public void OnDialogOpened(IDialogParameters parameters) { }
    }
}
