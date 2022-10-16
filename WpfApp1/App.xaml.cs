using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BookCathalog.Service;
using BookCathalog.Views;
using Prism.Services.Dialogs;
using BookCathalog.Dal;
using BookCathalog.Service;
using BookCathalog.ViewModels;

namespace BookCathalog
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<AddBookDialog,AddBookDialogViewModel>();
            containerRegistry.Register<ICustomerStore, DbCustomerStore>();
            containerRegistry.Register<IbookServise, BooksService>();
        }
        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }

        /*
         * TODO:
         * bd saves
         * bd read
         * unit tests
         * each property validation
         * 
         * 
         * MVVM model relations
         * 
         * cleanup

        */
    }
}
