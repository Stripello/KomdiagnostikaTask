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
        public void AddBook(Book book);
    }
}
