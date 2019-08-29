using Library.Bl.Model;
using Library.Bl.Repositories;
using Library.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Bl.Commands
{
    public class BorrowBookCommand: ICommand
    {
        private IBookRepository _bookRepository;
        private IUserRepository _userRepository;
        private IConsoleOperator _consoleOperator;

        public BorrowBookCommand(
            IBookRepository bookRepository,
            IUserRepository userRepository,
            IConsoleOperator consoleOperator)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _consoleOperator = consoleOperator;
        }

        public string Description => "Borrow book.";

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
            }
            else if(foundedBooks.All(book => book.IsCurrentBorrowed))
            {
                _consoleOperator.WriteLine($"Sorry, all books are borrowed!");
            }
            else 
            {
                var bookToBorrow = foundedBooks.FirstOrDefault(book => !book.IsCurrentBorrowed);
                bookToBorrow.CurrentBorrowing = new Borrowing(user, DateTime.UtcNow);
                _consoleOperator.WriteLine($"Success! {user.FirstName} , you borrow a book! ");
            }
        }
    }
}
