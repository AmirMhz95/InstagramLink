using System.Collections.Generic;
using System.Linq;
using InstagramLink.Data;
using InstagramLink.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InstagramLink.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool AddUser(User user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                _logger.LogWarning("User already exists: {Username}", user.Username);
                return false; // User already exists
            }
            _context.Users.Add(user);
            return _context.SaveChanges() > 0;
        }

        public User? GetUserById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid User ID: {UserId}", id);
                return null; // Invalid ID case
            }

            var user = _context.Users.Find(id);
            if (user == null)
            {
                _logger.LogWarning("User not found: {UserId}", id);
            }
            else
            {
                _logger.LogInformation("User found by ID: {UserId}", id);
            }
            return user;
        }

        public User? GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                _logger.LogWarning("Empty username provided for lookup.");
                return null; // or throw an exception
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                _logger.LogWarning("User not found: {Username}", username);
            }
            else
            {
                _logger.LogInformation("User found: {Username}", username);
            }
            return user;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}
