using Library.Bl.Model;
using Library.Bl.Repositories;
using Library.Cli;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Bl
{
    public class AddBookCommand : ICommand
    {
        private IBookRepository _bookShelfRepository;
        private IConsoleOperator _consoleOperator;

        public AddBookCommand(
            IBookRepository bookShelfRepository,
            IConsoleOperator consoleOperator)
        {
            _bookShelfRepository = bookShelfRepository;
            _consoleOperator = consoleOperator;
        }

        public string Description => "Add a book.";

        public void Execute()
        {
            var (title, author, quantity, isValid) = _consoleOperator.GetBookDescriptionWithQuantity();

            if (!isValid)
                return;

             _bookShelfRepository.AddBook(title, author, quantity);

            _consoleOperator.ShowSummaryOfAddedBook(quantity, title, author);
        }
    }
}
