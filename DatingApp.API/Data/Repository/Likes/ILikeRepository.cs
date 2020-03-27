using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data.Repository.Likes
{
    public interface ILikeRepository
    {
         Task<Like> GetLike(int userId, int recipientId);
    }
}