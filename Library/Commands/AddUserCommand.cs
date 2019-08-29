using Library.Bl.Model;
using Library.Bl.Repositories;
using Library.Cli;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Bl
{
    public class AddUserCommand : ICommand
    {
        private IUserRepository _userRepository;
        private IConsoleOperator _consoleOperator;

        public AddUserCommand(
            IUserRepository userRepository,
            IConsoleOperator consoleOperator)
        {
            _userRepository = userRepository;
            _consoleOperator = consoleOperator;
        }

        public string Description => "Add a user.";

        public void Execute()
        {
            var (firstName, lastName, isValid) = _consoleOperator.GetUser();

            if (!isValid)
                return;

            var newUser = _userRepository.GetUser(firstName, lastName);
            if (newUser == null)
            {
                _userRepository.AddUser(firstName, lastName);
                _consoleOperator.WriteLine("Succeded");
                return;
            }
            _consoleOperator.WriteLine("A user already exists!");
        }
    }
}
