using InstagramLink.Models;

namespace InstagramLink.Services
{
    public interface IUserService
    {
        bool Register(User user);
        User GetUser(int id);
        bool Login(string username, string password);
        List<User> GetAllUsers();


    }
}