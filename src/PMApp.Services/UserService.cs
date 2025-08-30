using PMApp.Data;
using PMApp.Data.Entities;
using PMApp.Data.Enums;
using PMApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserManager _userManager;

        public UserService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userManager.GetAllAsync();
        }

        public async Task<User> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User> GetUserByUserName(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
        public async Task<bool> CreateUser(string userName, string password, string firstName, string lastName)
        {
            if (await GetUserByUserName(userName) != null)
            {
                return false;
            }

            User user = new User()
            { 
                UserName = userName,
                PasswordHash = password,
                FirstName = firstName,
                LastName = lastName,
            };

            await _userManager.CreateUserAsync(user, password);

            return true;
        }

        public async Task<User> GetCurrentUser(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }

        public async Task<bool> IsUserInRole(string userId, string roleName)
        {
            return await _userManager.IsUserInRole(userId, roleName);
        }

        public async Task<bool> Update(string id, User user)
        {
            User editingUser = await _userManager.FindByIdAsync(id);
            if (editingUser == null)
            {
                return false;
            }

            editingUser.UserName = user.UserName;
            editingUser.FirstName = user.FirstName;
            editingUser.LastName = user.LastName;
            await _userManager.UpdateUser(editingUser);

            return true;
        }

        public async Task Delete(User user)
        {
            await _userManager.DeleteUser(user);
        }

    }
}
