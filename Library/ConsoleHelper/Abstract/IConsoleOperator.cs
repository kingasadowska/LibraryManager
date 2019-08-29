using System.Collections.Generic;
using Library.Bl.Model;

namespace Library.Cli
{
    public interface IConsoleOperator
    {
        void CreateRaport(IEnumerable<Report> reports, int bookCount);
        (string, string, bool) GetBookDescription();
        (string, string, uint, bool) GetBookDescriptionWithQuantity();
        (string, string, bool) GetUser();
        string Read();
        void ShowSummaryOfAddedBook(uint quantity, string title, string author);
        void WriteLine(string value);
    }
}