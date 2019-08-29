using Library.Bl.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Bl.Commands
{
    public class ExitCommand : ICommand
    {
        public string Description => "Exit.";

        public void Execute()
        {
            Environment.Exit(0);
        }
    }
}
