using BookCathalog.Dal.Models;
using System.Collections.Generic;

namespace BookCathalog.Service
{
    public interface IBookService
    {
        public IList<Book> GetAll();
        public void AddBook(Book book);
        public void DeleteBook(Book book);
        public void UpdateBook(Book oldBook, Book newBook);
    }
}
