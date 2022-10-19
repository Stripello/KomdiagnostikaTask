using BookCathalog.Dal.Models;
using System.Collections.Generic;

namespace BookCathalog.Service
{
    public interface IBookService
    {
        public IList<Book> GetAll();
        public void AddBook(Book book);
        public void DeleteBook(int bookId);
        public void UpdateBook(int oldBookId, Book newBook);
    }
}
