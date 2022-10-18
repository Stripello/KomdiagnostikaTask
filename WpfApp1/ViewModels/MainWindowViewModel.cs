using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookCathalog.Service;
using Prism.Services.Dialogs;
using BookCathalog.Dal.Models;
using System.Runtime.CompilerServices;

namespace BookCathalog.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IDialogService _dialogService;
        private IbookServise _ibookServise;
        public ObservableCollection<Book> AllBooks { get; private set; } = new ObservableCollection<Book>();

        public MainWindowViewModel(IDialogService dialogService, IbookServise bookService)
        {
            _dialogService = dialogService;
            _ibookServise = bookService;
            AllBooks.AddRange(_ibookServise.GetAll());
        }

        private Book _selectedBook = null;
        public Book SelectedBook
        {
            get => _selectedBook;
            set => SetProperty<Book>(ref _selectedBook, value);
        }

        private DelegateCommand _commandLoad;
        public DelegateCommand CommandLoad =>
            _commandLoad ?? (_commandLoad = new DelegateCommand(CommandLoadExecute));

        private void CommandLoadExecute()
        {
            _dialogService.ShowDialog("AddBookDialog");
            var newOne = _ibookServise.GetAll();
            AllBooks.AddRange(newOne.Except(AllBooks));
        }

        private DelegateCommand _commandDelete;
        public DelegateCommand CommandDelete =>
            _commandDelete ?? (_commandDelete = new DelegateCommand(CommandDeleteExecute));
        private void CommandDeleteExecute()
        {
            _ibookServise.DeleteBook(_selectedBook);
            AllBooks.Remove(_selectedBook);
        }

        private DelegateCommand _commandEdit;
        public DelegateCommand CommandEdit =>
            _commandEdit ?? (_commandEdit = new DelegateCommand(CommandUpdateExecute));
        private void CommandUpdateExecute()
        {
            var param = new DialogParameters();
            param.Add("currentBook", _selectedBook);
            _dialogService.ShowDialog("EditBookDialog", param, Callback);
            AllBooks.Clear();
            AllBooks.AddRange(_ibookServise.GetAll());

            void Callback(IDialogResult result)
            {
                //tcs.SetResult(result.Parameters.GetValue<bool>("confirmed"));
            }
        }
       
    }
}