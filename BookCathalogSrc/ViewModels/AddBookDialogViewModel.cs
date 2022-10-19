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
        public AddBookDialogViewModel(IbookServise bookService, IImageProcessor imageProcessor)
        {
            _bookServise = bookService;
            _imageProcessor = imageProcessor;
        }
        private IImageProcessor _imageProcessor;
        private IbookServise _bookServise;
        private IDialogResult _dialogResult;
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
            _dialogResult = new DialogResult();
            _dialogResult.Parameters.Add("addedBook",CurrentBook);
            RaiseRequestClose(_dialogResult);
        }
        public bool CanAddBook()
        {
            return (CurrentBook.Error == null || CurrentBook.Error.Length == 0);
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
        public bool CanCloseDialog() { return true;}
        public void OnDialogClosed() {}
        public void OnDialogOpened(IDialogParameters parameters) {}
        private void OnAttach() {}
    }
}
