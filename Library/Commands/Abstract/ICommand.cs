using Library.Bl.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Bl
{
    public interface ICommand
    {
        string Description { get; }

        void Execute();
    }
};
