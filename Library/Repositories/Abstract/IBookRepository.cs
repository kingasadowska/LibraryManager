using System.Collections.Generic;
using Library.Bl.Model;

namespace Library.Bl.Repositories
{
    public interface IBookRepository
    {
        List<Book> Book { get; set; }
        void AddBook(string title, string author, uint quantity);
        IEnumerable<IEnumerable<Book>> GetAllBooksWithCopiesForSpecifyAuthorWithTitle();
        IEnumerable<Book> GetBooks(string title, string author);
    }
}