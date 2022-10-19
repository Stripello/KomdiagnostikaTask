using BookCathalog.Dal.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace BookCathalog.Service
{
    public class BooksServiceLiteDb : IBookService
    {
        //Must be set by business in app.config file.
        const string _defaultDbFileName = "BooksLiteDatabase.db";
        private readonly string _dbLocation;
        private readonly string _dbName;
        public BooksServiceLiteDb( string dbName, string dbFileName = _defaultDbFileName,string dbFolder = "")
        {
            _dbLocation = string.Empty;
            if (Directory.Exists(dbFolder))
            {
                _dbLocation += dbFolder;
            }
            else
            {
                _dbLocation += Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                _dbLocation += @"\Data";
            }
            _dbLocation += @$"\{dbFileName}";
            _dbName = dbName;
        }

        public void DeleteBook(Book book)
        {
            using (var db = new LiteDatabase(_dbLocation))
            {
                var storedBooks = db.GetCollection<Book>(_dbName);
                storedBooks.DeleteMany(x => x.Guid == book.Guid && x.Author == book.Author && x.Isbn == book.Isbn);
            }
        }

        public void UpdateBook(Book oldBook, Book newBook)
        {
            using (var db = new LiteDatabase(_dbLocation))
            {
                var storedBooks = db.GetCollection<Book>(_dbName);
                var book = storedBooks.FindById(oldBook.Id);
                
                book.Title = newBook.Title;
                book.Author = newBook.Author;
                book.About = newBook.About;
                book.Guid = newBook.Guid;
                book.Isbn = newBook.Isbn;
                book.Year = newBook.Year;
                book.FrontPage = newBook.FrontPage;
                
                storedBooks.Update(book);
            }
        }

        public void AddBook(Book book)
        {
            using (var db = new LiteDatabase(_dbLocation))
            {
                var storedBooks = db.GetCollection<Book>(_dbName);
                storedBooks.Insert(book);
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
