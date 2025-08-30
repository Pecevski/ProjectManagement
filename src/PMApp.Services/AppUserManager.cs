using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PMApp.Data.Entities;
using PMApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMApp.Services
{
    public class AppUserManager : UserManager<User>, IUserManager
    {
        public AppUserManager(IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger) :
            base(store,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger)
        {

        }
        public async Task<List<User>> GetAllAsync()
        {
            return await Users.ToListAsync();
        }

        public async Task CreateUserAsync(User user, string password)
        {
            var result = await CreateAsync(user, password);
        }

        public async Task<List<string>> GetUserRolesAsync(User user)
        {
            return (await GetRolesAsync(user)).ToList();
        }

        public async Task<bool> IsUserInRole(string userId, string roleName)
        {
            User user = await FindByIdAsync(userId);
            return await IsInRoleAsync(user, roleName);
        }

        public async Task<bool> ValidateUserCredentials(string userName, string password)
        {
            User user = await FindByNameAsync(userName);
            if (UserNotNull(user))
            {
                bool result = await CheckPasswordAsync(user, password);
                return result;
            }
            return false;
        }

        private static bool UserNotNull(User user)
        {
            return user != null;
        }

        public async Task DeleteUser(User user)
        {
            await DeleteAsync(user);
        }

        public async Task UpdateUser(User user)
        {
            await UpdateAsync(user);
        }


    }
}
