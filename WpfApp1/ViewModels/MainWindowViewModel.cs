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

namespace BookCathalog.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
       // private ICustomerStore _customerStore;
        private IDialogService _dialogService;

        public MainWindowViewModel(/*ICustomerStore customerStore*/ IDialogService dialogService)
        {
            //_customerStore = customerStore;
            _dialogService = dialogService;
        }

        public ObservableCollection<string> Customers { get; private set; } = new ObservableCollection<string>();

        private string _selectedCustomer = null;
        public string SelectedCustomer
        {
            get => _selectedCustomer;
            set => SetProperty<string>(ref _selectedCustomer, value);
        }
        


        private DelegateCommand _commandLoad;
        public DelegateCommand CommandLoad =>
            _commandLoad ?? (_commandLoad = new DelegateCommand(CommandLoadExecute));

        private void CommandLoadExecute()
        {
            _dialogService.ShowDialog("AddBookDialog");
        }
    }
}