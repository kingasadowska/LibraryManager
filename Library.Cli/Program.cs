using Library.Bl;
using Library.Bl.Commands;
using Library.Bl.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Library.Cli
{
    public class Program
    {
        static void Main(string[] args)
        {
            var userRepository = new UserRepository();
            var bookRepository = new BookRepository();
            var consoleOperator = new ConsoleOperator();

            var options = new ReadOnlyCollection<ICommand>(new ICommand[]
            {
                new ExitCommand(),
                new AddUserCommand(userRepository, consoleOperator),
                new AddBookCommand(bookRepository, consoleOperator),
                new BorrowBookCommand(bookRepository, userRepository, consoleOperator),
                new ReturnBookCommand(bookRepository, userRepository, consoleOperator),
                new ReportCommand(bookRepository, consoleOperator),
            });

            new Menu(options).Run();
        }
    }
}