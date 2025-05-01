using ServiceLayer.DTO;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IAuthService
    {
        Task<bool> UserExists(string username);
        Task<UserDto> Register(string username, string email, string password);
        Task<string> Login(string username, string password);
    }
}
