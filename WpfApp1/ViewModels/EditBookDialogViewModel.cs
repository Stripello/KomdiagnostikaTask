using BookCathalog.Dal.Models;
using BookCathalog.Service;
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
        public EditBookDialogViewModel(IbookServise bookService, IImageProcessor imageProcessor)
        {
            _bookServise = bookService;
            _imageProcessor = imageProcessor;
        }
        private IImageProcessor _imageProcessor;
        private IbookServise _bookServise;
        private Book _currentBook  = new Book();
        public Book CurrentBook
        {
            get => _currentBook;
            set => SetProperty<Book>(ref _currentBook, value);
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("currentBook"))
            {
                _currentBook = parameters.GetValue<Book>("currentBook");
            }
        }

        //Some neceserry Prism stuff.
        public string Title { get; set; } = "Adding book";

        public event Action<IDialogResult> RequestClose;
        public bool CanCloseDialog() { return true; }
        public void OnDialogClosed() { }
        
    }
}
