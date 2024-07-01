using Microsoft.Graph;
using System;
using System.Threading.Tasks;
using UserMicroservice.Models;

namespace UserMicroservice.Services
{
    public class GraphService : IGraphService
    {
        private readonly GraphServiceClient _graphServiceClient;

        public GraphService(GraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }

        Task<User> IGraphService.GetUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        //public async Task<User> GetUserByIdAsync(string userId)
        //{
        //    try
        //    {

        //        var user = await _graphServiceClient.Users[userId]
        //            .Request()
        //            .GetAsync();
        //        return user;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle or log the exception as needed
        //        throw new Exception("Error retrieving user from Microsoft Graph", ex);
        //    }
        //}

    }
}
