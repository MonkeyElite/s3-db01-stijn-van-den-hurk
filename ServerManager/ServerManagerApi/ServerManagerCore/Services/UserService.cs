using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;

namespace ServerManagerCore.Services
{
    public class UserService(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }

        public async Task<(bool Success, string Message, User User)> RegisterUserAsync(User user)
        {
            // Check if user already exists
            var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return (false, "User already exists.", null);
            }

            // Add user to the repository
            await _userRepository.AddUserAsync(user);

            return (true, "User registered successfully.", user);
        }
    }
}
