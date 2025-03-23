using CarManagementSystem.Models;

namespace CarManagementSystem.Services
{
    public interface IAuthService
    {
        User Authenticate(string username, string password);
        string GenerateJwtToken(User user);
    }
}
