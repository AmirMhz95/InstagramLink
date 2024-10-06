using InstagramLink.Models;
using InstagramLink.Repositories;
using Microsoft.Extensions.Logging;


namespace InstagramLink.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;


        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public bool Register(User user)
        {
            // Add validation and hashing logic here
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                _logger.LogWarning("Invalid user data");
                return false;
            }

            if (_userRepository.GetUserByUsername(user.Username) != null)
            {
                _logger.LogWarning("User already exists");
                return false;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _logger.LogInformation("Hashed Password: {HashedPassword}", user.Password);

            // Add user to the repository
            var result = _userRepository.AddUser(user);
            if (result)
            {
                _logger.LogInformation("User registered successfully");
            }
            else
            {
                _logger.LogWarning("User registration failed");
            }
            return result;
        }

        public bool Login(string username, string password)
        {
            _logger.LogInformation("Login attempt for username: {Username}", username);
            var user = _userRepository.GetUserByUsername(username);
            if (user != null)
            {
                _logger.LogInformation("User found: {Username}", username);
                _logger.LogInformation("Stored Hashed Password: {StoredHashedPassword}", user.Password);
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    _logger.LogInformation("Password verification successful");
                    return true;
                }
                else
                {
                    _logger.LogWarning("Password verification failed");
                }
            }
            else
            {
                _logger.LogWarning("User not found: {Username}", username);
            }
            return false;
        }

        public User? GetUser(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

    }
}