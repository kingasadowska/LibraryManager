using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Bl.Model
{
    public class Report
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int CountOfAvailableBooks { get; set; }
        public int CountOfBooks { get; set; }
    }
}
