using Library.Bl.Model;
using Library.Bl.Repositories;
using Library.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Bl.Commands
{
    public class ReportCommand : ICommand
    {
        private IBookRepository _bookRepository;
        private IConsoleOperator _consoleOperator;

        public ReportCommand(
            IBookRepository bookRepository,
            IConsoleOperator consoleOperator)
        {
            _bookRepository = bookRepository;
            _consoleOperator = consoleOperator;
        }

        public string Description => "Report.";

        public void Execute()
        {
           var allBooksWithCopies = _bookRepository.GetAllBooksWithCopiesForSpecifyAuthorWithTitle()
                .Select(groupedListOfBook => new Report()
                {
                    Title = groupedListOfBook.First().BookDescription.Title,
                    Author = groupedListOfBook.First().BookDescription.Author,
                    CountOfAvailableBooks = groupedListOfBook.Where(book => !book.IsCurrentBorrowed).Count(),
                    CountOfBooks = groupedListOfBook.Count()
                });

            _consoleOperator.CreateRaport(allBooksWithCopies, _bookRepository.Book.Count());    
        }
    }
}
