using BookCathalog.Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCathalog.Service
{
    public interface IbookServise
    {
        public IList<Book> GetAll();
        public TypeOfError AddBook(Book book); 
    }

    public class BooksService : IbookServise
    {
        public IList<Book> books = new List<Book>();
        public TypeOfError AddBook(Book book)
        {
            var answer = book.Validate();
            if (answer == TypeOfError.NoError)
            {
                books.Add(book);
            }
            return answer;
        }

        public IList<Book> GetAll()
        {
            return books;
        }
    }
}
