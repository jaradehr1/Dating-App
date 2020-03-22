using System.Threading.Tasks;

namespace DatingApp.API.Data.Repository.General
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveChangesAsync();
    }
}