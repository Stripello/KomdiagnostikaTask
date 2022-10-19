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
            var dbFileName = ConfigurationManager.AppSettings["litedbfilename"];
            var dbName = ConfigurationManager.AppSettings["dbname"];
            containerRegistry.RegisterDialog<AddBookDialog, AddBookDialogViewModel>();
            containerRegistry.RegisterDialog<EditBookDialog, EditBookDialogViewModel>(); 
            containerRegistry.RegisterInstance<IBookService>(new BooksServiceLiteDb(dbName,dbFileName));
            containerRegistry.RegisterSingleton<IImageProcessor,ImageProcessor>();
        }
        protected override Window CreateShell()
        {
            var w = Container.Resolve<MainWindow>();
            return w;
        }

        /*
         * TODO:
         * update view list on fly after adding and updating elements
         * mute buttons
         * closing after save new book
         * unit tests
         * maximal year above current in appconfig
         * show error types
         * 
         * MVVM model relations
         * 
         * cleanup

        */
    }
}
