using BookCathalog.Dal.Models;
using BookCathalog.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace BookCathalog.ViewModels
{
    public class AddBookDialogViewModel : BindableBase, IDialogAware
    {
        public AddBookDialogViewModel(IBookService bookService, IImageProcessor imageProcessor)
        {
            _bookServise = bookService;
            _imageProcessor = imageProcessor;
        }

        private IImageProcessor _imageProcessor;
        private IBookService _bookServise;
        private IDialogResult _dialogResult;
        public Book CurrentBook { get; } = new Book();

        private string _frontPageLocation = string.Empty;
        public string FrontPageLocation
        {
            get => _frontPageLocation;
            set => SetProperty<string>(ref _frontPageLocation, value);
        }

        private DelegateCommand _addBookCommand;
        public DelegateCommand AddBookCommand =>
            _addBookCommand ?? (_addBookCommand = new DelegateCommand(AddBook, () => CurrentBook.IsValid()));

        private void AddBook()
        {
            _bookServise.AddBook(CurrentBook);
            _dialogResult = new DialogResult();
            _dialogResult.Parameters.Add("addedBook", CurrentBook);
            RaiseRequestClose(_dialogResult);
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

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        //Some neceserry Prism stuff.
        public string Title { get; set; } = "Adding book";

        public event Action<IDialogResult> RequestClose;
        public bool CanCloseDialog() { return true; }
        public void OnDialogClosed()
        {
            CurrentBook.PropertyChanged -= CurrentBook_PropertyChanged;
        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            CurrentBook.PropertyChanged += CurrentBook_PropertyChanged;
        }
        private void CurrentBook_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            AddBookCommand.RaiseCanExecuteChanged();
        }
    }
}
