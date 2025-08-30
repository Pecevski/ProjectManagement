using PMApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Services.Contracts
{
    public interface IUserManager
    {

        Task<User> GetUserAsync(ClaimsPrincipal claimsPrincipal);
        Task<bool> IsUserInRole(string userId, string roleName);
        Task<User> FindByNameAsync(string username);
        Task<User> FindByIdAsync(string id);
        Task<List<User>> GetAllAsync();
        Task CreateUserAsync(User user, string password);
        Task<List<string>> GetUserRolesAsync(User user);
        Task<bool> ValidateUserCredentials(string userName, string password);
        public Task UpdateUser(User user);
        public Task DeleteUser(User user);
    }
}
