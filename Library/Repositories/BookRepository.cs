using Library.Bl.Model;
using System.Collections.Generic;
using System.Linq;

namespace Library.Bl.Repositories
{
    public class BookRepository : IBookRepository
    {
        public List<Book> Book { get; set; }

        public BookRepository()
        {
            Book = new List<Book>();
        }

        public IEnumerable<IEnumerable<Book>> GetAllBooksWithCopiesForSpecifyAuthorWithTitle()
        {
            return Book.GroupBy(x => new { x.BookDescription.Author, x.BookDescription.Title }).Select(grp => grp.ToList()).ToList();
        }

        public void AddBook(string title, string author, uint quantity)
        {
            var bookDescripton = new BookDescription(title, author);
            var bookShelf = new Book(bookDescripton);
            var collectionOfTheSameBooks = Enumerable.Range(0, (int)quantity).Select(x => bookShelf);
            Book.AddRange(collectionOfTheSameBooks);
        }

        public IEnumerable<Book> GetBooks(string title, string author)
        {
            return Book
              .Where(book =>
                  book.BookDescription.Title == title &&
                  book.BookDescription.Author == author);
        }
    }
}
