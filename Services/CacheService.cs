using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using UserMicroservice.Models;
using System.Threading.Tasks;

namespace UserMicroservice.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<User> GetUserAsync(int id)
        {
            var cachedUser = await _cache.GetStringAsync(id.ToString());
            return cachedUser == null ? null : JsonSerializer.Deserialize<User>(cachedUser);
        }

        public async Task SetUserAsync(User user)
        {
            var serializedUser = JsonSerializer.Serialize(user);
            await _cache.SetStringAsync(user.Id.ToString(), serializedUser);
        }

        public async Task RemoveUserAsync(int id)
        {
            await _cache.RemoveAsync(id.ToString());
        }
    }
}
