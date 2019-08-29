using System;
using Xunit;
using Library.Bl;
using Library.Bl.Repositories;
using Library.Cli;
using Moq;
using System.Linq;
using Library.Bl.Commands;
using Library.Bl.Model;

namespace UnitTest
{
    public class UserCommandTests
    {
        [Fact]
        public void ShouldAddUserToRepository()
        {
            var userRepository = new UserRepository();
            var firstName = "Kinga";
            var surName = "Sadowska";

            var consoleOperator = new Mock<IConsoleOperator>();
            consoleOperator.Setup(x => x.GetUser()).Returns((firstName, surName, true));
            consoleOperator.Setup(x => x.WriteLine(It.IsAny<string>()));

            var addUserCommand = new AddUserCommand(userRepository, consoleOperator.Object);
            addUserCommand.Execute();

            Assert.Single(userRepository.Users);
            Assert.Contains(userRepository.Users.FirstOrDefault().FirstName, firstName);
            Assert.Contains(userRepository.Users.FirstOrDefault().LastName, surName);
        }

        [Fact]
        public void ShouldAddBookrToRepository()
        {
            var bookRepository = new BookRepository();
            uint quantity = 1;
            var title = "A Game of Thrones";
            var author = "George R. R. Martin";

            var consoleOperator = new Mock<IConsoleOperator>();
            consoleOperator.Setup(x => x.GetBookDescriptionWithQuantity()).Returns((title, author, quantity, true));

            var addBookCommand = new AddBookCommand(bookRepository, consoleOperator.Object);
            addBookCommand.Execute();

            Assert.Single(bookRepository.Book);
            Assert.Equal(bookRepository.Book.Count, (int)quantity);
            Assert.Contains(bookRepository.Book.FirstOrDefault().BookDescription.Title, title);
            Assert.Contains(bookRepository.Book.FirstOrDefault().BookDescription.Author, author);
        }

        [Fact]
        public void ShouldBorrowBookToRepository()
        {
            var userRepository = new UserRepository();
            var firstName = "Kinga";
            var surName = "Sadowska";

            var bookRepository = new BookRepository();
            var title = "A Game of Thrones";
            var author = "George R. R. Martin";

            var quantity = 1;

            var consoleOperator = new Mock<IConsoleOperator>();
            consoleOperator.Setup(x => x.GetUser()).Returns((firstName, surName, true));
            consoleOperator.Setup(x => x.WriteLine(It.IsAny<string>()));
            consoleOperator.Setup(x => x.GetBookDescription()).Returns((title, author, true));
            consoleOperator.Setup(x => x.GetBookDescriptionWithQuantity()).Returns((title, author, (uint)quantity, true));

            var addUserCommand = new AddUserCommand(userRepository, consoleOperator.Object);
            addUserCommand.Execute();

            var addBookCommand = new AddBookCommand(bookRepository, consoleOperator.Object);
            addBookCommand.Execute();

            Assert.Single(userRepository.Users);
            Assert.Single(bookRepository.Book);

            var borrowBookCommand = new BorrowBookCommand(bookRepository, userRepository, consoleOperator.Object);
            borrowBookCommand.Execute();

            Book first = null;
            foreach (var book in bookRepository.Book)
            {
                first = book;
                break;
            }

            Assert.Equal(first.CurrentBorrowing.User.FirstName, firstName);
            Assert.Equal(bookRepository.Book.FirstOrDefault().CurrentBorrowing.User.LastName, surName);
            Assert.True(bookRepository.Book.FirstOrDefault().IsCurrentBorrowed);
        }

        [Fact]
        public void ShouldReturnBookToRepository()
        {
            var userRepository = new UserRepository();
            var firstName = "Kinga";
            var surName = "Sadowska";

            var bookRepository = new BookRepository();
            var title = "A Game of Thrones";
            var author = "George R. R. Martin";

            var quantity = 1;

            var consoleOperator = new Mock<IConsoleOperator>();
            consoleOperator.Setup(x => x.GetUser()).Returns((firstName, surName, true));
            consoleOperator.Setup(x => x.WriteLine(It.IsAny<string>()));
            consoleOperator.Setup(x => x.GetBookDescription()).Returns((title, author, true));
            consoleOperator.Setup(x => x.GetBookDescriptionWithQuantity()).Returns((title, author, (uint)quantity, true));

            var addUserCommand = new AddUserCommand(userRepository, consoleOperator.Object);
            addUserCommand.Execute();

            var addBookCommand = new AddBookCommand(bookRepository, consoleOperator.Object);
            addBookCommand.Execute();

            Assert.Single(userRepository.Users);
            Assert.Single(bookRepository.Book);

            var returnBookCommand = new BorrowBookCommand(bookRepository, userRepository, consoleOperator.Object);
            returnBookCommand.Execute();

            Assert.Equal(bookRepository.Book.FirstOrDefault().CurrentBorrowing.User.FirstName, firstName);
            Assert.Equal(bookRepository.Book.FirstOrDefault().CurrentBorrowing.User.LastName, surName);
            Assert.True(bookRepository.Book.FirstOrDefault().IsCurrentBorrowed);
        }
    }
}
