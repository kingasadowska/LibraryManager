using Library.Bl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Cli
{

    public class ConsoleOperator : IConsoleOperator
    {
        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public string Read()
        {
            return Console.ReadLine();
        }

        public (string, string, uint, bool) GetBookDescriptionWithQuantity()
        {
            this.WriteLine("Type title:");
            string title = this.Read();

            this.WriteLine("Type author:");
            string author = this.Read();

            this.WriteLine("Type quantity:");
            string quantityFromConsole = this.Read();
            uint quantity = string.IsNullOrWhiteSpace(quantityFromConsole) ? 0 : Convert.ToUInt32(quantityFromConsole);

            bool isValid = true;
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) || quantity == 0)
            {
                this.WriteLine("You send me wrong data");
                isValid = false;
            }

            return (title, author, quantity, isValid);
        }

        public (string, string, bool) GetBookDescription()
        {
            this.WriteLine("Type title:");
            string title = this.Read();

            this.WriteLine("Type author:");
            string author = this.Read();

            bool isValid = true;
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                this.WriteLine("You send me wrong data");
                isValid = false;
            }

            return (title, author, isValid);
        }

        public (string, string, bool) GetUser()
        {
            this.WriteLine("Type name:");
            string firstName = this.Read();

            this.WriteLine("Type surname:");
            string lastName = this.Read();

            bool isValid = true;
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                this.WriteLine("You send me wrong data");
                isValid = false;
            }

            return (firstName, lastName, isValid);
        }

        public void ShowSummaryOfAddedBook(uint quantity, string title, string author)
        {
            this.WriteLine(
             $"Success! You add a new position. " +
             Environment.NewLine +
             $"Title: {title} " +
             Environment.NewLine +
             $"Author: {author} " +
             Environment.NewLine +
             $"Quantity: {quantity}");
        }

        public void CreateRaport(IEnumerable<Report> reports, int bookCount)
        {
            this.WriteLine($"Conut of all books in library: {bookCount} ");
            reports.ToList().ForEach(el => {
                this.WriteLine($"Book: {el.Author} : {el.Title} Copies Avaible: {el.CountOfAvailableBooks}/{el.CountOfBooks} ");
            });
        }
    }
}
