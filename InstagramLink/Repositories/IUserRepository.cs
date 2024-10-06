using InstagramLink.Models;

namespace InstagramLink.Repositories
{
    public interface IUserRepository
    {
        bool AddUser(User user);
        User GetUserById(int id);
        User GetUserByUsername(string username);
        List<User> GetAllUsers();
    }
}