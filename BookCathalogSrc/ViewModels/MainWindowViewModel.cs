using BookCathalog.Dal.Models;
using BookCathalog.Service;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;

namespace BookCathalog.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IDialogService _dialogService;
        private IBookService _ibookServise;
        public ObservableCollection<Book> AllBooks { get; private set; } = new ObservableCollection<Book>();

        public MainWindowViewModel(IDialogService dialogService, IBookService bookService)
        {
            _dialogService = dialogService;
            _ibookServise = bookService;
            AllBooks.AddRange(_ibookServise.GetAll());
        }

        private Book _selectedBook = null;
        public Book SelectedBook
        {
            get => _selectedBook;
            set 
            {
                SetProperty<Book>(ref _selectedBook, value);
                CommandEdit.RaiseCanExecuteChanged();
            }
                
        }

        private DelegateCommand _commandLoad;
        public DelegateCommand CommandLoad =>
            _commandLoad ?? (_commandLoad = new DelegateCommand(CommandLoadExecute));

        private void CommandLoadExecute()
        {
            _dialogService.ShowDialog("AddBookDialog", CommandLoadCallback);
        }
        private void CommandLoadCallback(IDialogResult results)
        {
            if (results == null)
            {
                return;
            }
            if (results.Parameters.ContainsKey("addedBook"))
            {
                var addedBook = results.Parameters.GetValue<Book>("addedBook");
                AllBooks.Add(addedBook);
            }
        }

        private DelegateCommand _commandDelete;
        public DelegateCommand CommandDelete =>
            _commandDelete ?? (_commandDelete = new DelegateCommand(CommandDeleteExecute));
        private void CommandDeleteExecute()
        {
            _ibookServise.DeleteBook(_selectedBook.Id);
            AllBooks.Remove(_selectedBook);
        }

        private DelegateCommand _commandEdit;
        public DelegateCommand CommandEdit =>
            _commandEdit ?? (_commandEdit = new DelegateCommand(CommandUpdateExecute, ()=>_selectedBook!=null));
        private void CommandUpdateExecute()
        {
            var param = new DialogParameters();
            param.Add("currentBook", _selectedBook);
            _dialogService.ShowDialog("EditBookDialog", param, Callback);
            AllBooks.Clear();
            AllBooks.AddRange(_ibookServise.GetAll());
            void Callback(IDialogResult result) { }
        }

    }
}