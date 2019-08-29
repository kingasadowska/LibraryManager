using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Bl.Model
{
    public class Book
    {
        public Book(BookDescription bookDescription)
        {
            BookDescription = bookDescription;
        }

        public BookDescription BookDescription { get; set; }
        public Borrowing CurrentBorrowing { get; set; }
        public List<Borrowing> HistoryOfBorrowings = new List<Borrowing>();
        public bool IsCurrentBorrowed => CurrentBorrowing != null;
    }
}
