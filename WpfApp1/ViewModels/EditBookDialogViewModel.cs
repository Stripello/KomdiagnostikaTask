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



        //Some neceserry Prism stuff.
        public string Title { get; set; } = "Adding book";

        public event Action<IDialogResult> RequestClose;
        public bool CanCloseDialog() { return true; }
        public void OnDialogClosed() { }
        public void OnDialogOpened(IDialogParameters parameters) { }
    }
}
