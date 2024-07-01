using Microsoft.Graph;
using System.Threading.Tasks;
using UserMicroservice.Models;

namespace UserMicroservice.Services
{
    public interface IGraphService
    {
        Task<User> GetUserByIdAsync(string userId);
    }
}
