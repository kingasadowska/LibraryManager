using Library.Bl.Model;
using System.Collections.Generic;
using System.Linq;

namespace Library.Bl.Repositories
{
    public class UserRepository : IUserRepository
    {
        public List<User> Users { get; set; }

        public UserRepository()
        {
            Users = new List<User>();
        }

        public User GetUser(string firstName, string lastName)
        {
            return Users
               .FirstOrDefault(userDetails =>
               userDetails.FirstName == firstName &&
               userDetails.LastName == lastName);
        }

        public void AddUser(string firstName, string lastName)
        {
            Users.Add(new User(firstName, lastName));
        }
    }
}
