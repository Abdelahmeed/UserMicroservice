using UserMicroservice.Models;
using System.Threading.Tasks;

namespace UserMicroservice.Services
{
    public interface ICacheService
    {
        Task<User> GetUserAsync(int id);
        Task SetUserAsync(User user);
        Task RemoveUserAsync(int id);
    }
}
