using BookCathalog.Dal.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace BookCathalog.Service
{
    public class BooksServiceLiteDb : IbookServise
    {
        //Must be set by business in app.config file.
        const string _defaultDbName = "BooksLiteDatabase";
        private readonly string _dbLocation;
        private readonly string _dbName;
        public BooksServiceLiteDb(string dbName, string dbFileName = _defaultDbName, string dbFolder = "")
        {
            var _dbLocation = string.Empty;
            if (Directory.Exists(dbFolder))
            {
                _dbLocation += dbFolder;
            }
            else
            {
                _dbLocation += Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                _dbLocation += @"\Data";
            }
            _dbLocation += @$"\{dbFileName}.db";
            _dbName = dbName;
        }
        public void AddBook(Book book)
        {
            using (var db = new LiteDatabase(_dbLocation))
            {
                var storedFileSystemNodes = db.GetCollection<Book>(_dbName);
                storedFileSystemNodes.Insert(book);
            }
        }

        public IList<Book> GetAll()
        {
            using (var db = new LiteDatabase(_dbLocation))
            {
                return db.GetCollection<Book>(_dbName).FindAll().ToList();
            }
        }
    }
}
