using BookCathalog.Dal.Models;
using BookCathalog.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCathalog.ViewModels
{
    public class EditBookDialogViewModel : BindableBase, IDialogAware
    {
        public EditBookDialogViewModel(IBookService bookService, IImageProcessor imageProcessor)
        {
            _bookServise = bookService;
            _imageProcessor = imageProcessor;
        }
        private IImageProcessor _imageProcessor;
        private IBookService _bookServise;

        private Book _oldBook = new Book();
        private Book _currentBook = new Book();
        public Book CurrentBook
        {
            get => _currentBook;
            set => SetProperty<Book>(ref _currentBook, value);
        }

        private DelegateCommand _editBookCommand;
        public DelegateCommand EditBookCommand =>
            _editBookCommand ?? (_editBookCommand =
            new DelegateCommand(() => _bookServise.UpdateBook(_oldBook, _currentBook)));

        private DelegateCommand _undoChangesCommand;
        public DelegateCommand UdnoChangesCommand =>
            _undoChangesCommand ?? (_undoChangesCommand =
            new DelegateCommand( () => { CurrentBook = _oldBook.CreateCopy();} ));

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("currentBook"))
            {
                _oldBook = parameters.GetValue<Book>("currentBook");
                _currentBook = _oldBook.CreateCopy();
            }
        }

        //Some neceserry Prism stuff.
        public string Title { get; set; } = "Edit book";

        public event Action<IDialogResult> RequestClose;
        public bool CanCloseDialog() { return true; }
        public void OnDialogClosed() { }
        
    }
}
