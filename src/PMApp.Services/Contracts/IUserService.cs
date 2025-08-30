using PMApp.Data.Entities;
using PMApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Services.Contracts
{
    public interface IUserService 
    {
        public Task<IEnumerable<User>> GetAll();
        public Task<User> GetById(string id);
        public Task<User> GetUserByUserName(string username);
        Task<User> GetCurrentUser(ClaimsPrincipal principal);
        Task<bool> IsUserInRole(string userId, string roleName);
        public Task<bool> CreateUser(string userName, string password, string firstName, string lastName);
        public Task<bool> Update(string id,User user);
        public Task Delete(User user);

    }
}
