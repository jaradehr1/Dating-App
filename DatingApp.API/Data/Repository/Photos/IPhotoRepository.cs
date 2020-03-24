using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data.Repository.Photos
{
    public interface IPhotoRepository
    {
         Task<Photo> GetPhoto(int id);

         Task<Photo> GetMainPhotoFroUser(int userId);
    }
}