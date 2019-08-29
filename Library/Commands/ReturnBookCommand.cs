using Library.Bl.Model;
using Library.Bl.Repositories;
using Library.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Bl.Commands
{
    public class ReturnBookCommand : ICommand
    {
        private IBookRepository _bookRepository;
        private IUserRepository _userRepository;
        private IConsoleOperator _consoleOperator;

        public ReturnBookCommand(
            IBookRepository bookRepository,
            IUserRepository userRepository,
            IConsoleOperator consoleOperator)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _consoleOperator = consoleOperator;
        }

        public string Description => "Return book.";

        public void Execute()
        {
            var (firstName, lastName, isValid) = _consoleOperator.GetUser();

            if (!isValid)
                return;

            var user = _userRepository.GetUser(firstName, lastName);

            if (user == null)
            {
                _consoleOperator.WriteLine($"We don't have you. Please go back to option 1. Add User");
                return;
            }

            var (title, author, isValidBookDescription) = _consoleOperator.GetBookDescription();

            if (!isValidBookDescription)
                return;

            var foundedBooks = _bookRepository.GetBooks(title, author);

            if (!foundedBooks.Any())
            {
                _consoleOperator.WriteLine($"Sorry, book not found!");
                return;
            }

            var findBookUserBorrow = foundedBooks.FirstOrDefault(book => book.IsCurrentBorrowed && book.CurrentBorrowing.User == user);

            if (findBookUserBorrow != null)
            {
                var borrowingTimeOfBook = findBookUserBorrow.CurrentBorrowing.BorrowTime;
                var returnTimeOfBook = DateTime.UtcNow;
                var historyOfReturnBook = new Borrowing(user, borrowingTimeOfBook, returnTimeOfBook);
                findBookUserBorrow.HistoryOfBorrowings.Add(historyOfReturnBook);
                findBookUserBorrow.CurrentBorrowing = null;
                _consoleOperator.WriteLine($"Success, you return book!");
                return;
            }
            _consoleOperator.WriteLine($"Sorry, you don't borrow this book!");
        }
    }
}
