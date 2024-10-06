using System.Collections.Generic;
using System.Linq;
using InstagramLink.Models;

namespace InstagramLink.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public bool AddUser(User user)
        {

            if (_users.Any(u => u.Username == user.Username))
            {
                return false; // User already exists
            }
            user.Id = _users.Count + 1; // Simple ID assignment

            _users.Add(user);
            return true;
        }

        public User? GetUserById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public User? GetUserByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }
    }
}