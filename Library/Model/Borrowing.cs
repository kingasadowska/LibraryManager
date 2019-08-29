using System;

namespace Library.Bl.Model
{
    public class Borrowing
    {
        public Borrowing(User user, DateTime borrowTime)
        {
            User = user;
            BorrowTime = borrowTime;
        }

        public Borrowing(User user, DateTime borrowTime, DateTime returnTime)
        {
            User = user;
            BorrowTime = borrowTime;
            ReturnTime = returnTime;
        }

        public User User { get; }
        public DateTime BorrowTime { get; }
        public DateTime? ReturnTime { get; set; }
    }
}
