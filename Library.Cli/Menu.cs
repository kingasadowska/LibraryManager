using Library.Bl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Library.Cli
{
    public class Menu
    {
        private readonly ReadOnlyCollection<ICommand> _options;

        public Menu(ReadOnlyCollection<ICommand> options)
        {
            _options = options;
        }

        public void Run()
        {
            while (true)
            {
                for (int i = 0; i < _options.Count; i++)
                    Console.WriteLine($"{i} - {_options[i].Description}");

                var input = Console.ReadLine();
                var isAbleToTryParse = uint.TryParse(input, out var choice);
                var isChoiceCorrect = choice <= _options.Count;

                if (isAbleToTryParse && isChoiceCorrect)
                    _options[(int)choice].Execute();

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
