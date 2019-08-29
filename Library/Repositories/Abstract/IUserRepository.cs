using Library.Bl.Model;
using System.Collections.Generic;

namespace Library.Bl.Repositories
{
    public interface IUserRepository
    {
        List<User> Users { get; set; }
        void AddUser(string firstName, string lastName);
        User GetUser(string firstName, string lastName);
    }
}