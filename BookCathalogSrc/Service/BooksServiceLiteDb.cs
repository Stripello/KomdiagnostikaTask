using BookCathalog.Dal.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookCathalog.Service
{
    public class BooksServiceLiteDb : IBookService
    {
        const string _defaultDbFileName = "BooksLiteDatabase.db";
        private readonly string _dbLocation;
        private readonly string _dbName;
        public BooksServiceLiteDb(string dbName, string dbFileName = _defaultDbFileName, string dbFolder = "")
        {
            if (!Directory.Exists(dbFolder))
            {
                var currentDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                dbFolder = Path.Combine(currentDirectory, "Data");
            }
            _dbLocation =Path.Combine(dbFolder,dbFileName);
            _dbName = dbName;
        }

        public void DeleteBook(int bookId)
        {
            using (var db = new LiteDatabase(_dbLocation))
            {
                var storedBooks = db.GetCollection<Book>(_dbName);
                storedBooks.Delete(bookId);
            }
        }

        public void UpdateBook(int oldBookId, Book newBook)
        {
            using (var db = new LiteDatabase(_dbLocation))
            {
                var storedBooks = db.GetCollection<Book>(_dbName);
                var book = storedBooks.FindById(oldBookId);

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
