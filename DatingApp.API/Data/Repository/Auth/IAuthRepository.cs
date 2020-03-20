using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data.Repository.Auth
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string useranme, string password);
         Task<bool> UsernameExists(string useranme);
    }
}