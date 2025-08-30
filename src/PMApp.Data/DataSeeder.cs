using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PMApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMApp.Data
{
    public class DataSeeder
    {
        public static void Seed(IServiceProvider applicationServices)
        {
            using (IServiceScope serviceScope = applicationServices.CreateScope())
            {
                PMDbContext context = serviceScope.ServiceProvider.GetRequiredService<PMDbContext>();
                if (context.Database.EnsureCreated())
                {
                    PasswordHasher<User> hasher = new PasswordHasher<User>();

                    User admin = new User()
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        UserName = "admin",
                        NormalizedUserName = "admin".ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        FirstName = "admin",
                        LastName = "admin"
                    };

                    admin.PasswordHash = hasher.HashPassword(admin, "adminpass");

                    IdentityRole identityRole = new IdentityRole()
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        Name = "Admin",
                        NormalizedName = "Admin".ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString("D")
                    };

                    IdentityUserRole<string> identityUserRole = new IdentityUserRole<string>() { RoleId = identityRole.Id, UserId = admin.Id };

                    context.Roles.Add(identityRole);
                    context.Users.Add(admin);
                    context.UserRoles.Add(identityUserRole);

                    User manager = new User()
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        UserName = "manager",
                        NormalizedUserName = "manager".ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString("D"),
                        FirstName = "manager",
                        LastName = "manager"

                    };

                    manager.PasswordHash = hasher.HashPassword(manager, "managerpass");

                    IdentityRole identityRoleManager = new IdentityRole()
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        Name = "Manager",
                        NormalizedName = "Manager".ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString("D")
                    };

                    IdentityUserRole<string> identityUserRoleManager = new IdentityUserRole<string>() { RoleId = identityRoleManager.Id, UserId = manager.Id };

                    context.Roles.Add(identityRoleManager);
                    context.Users.Add(manager);
                    context.UserRoles.Add(identityUserRoleManager);

                    IdentityRole identityRoleUser = new IdentityRole()
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        Name = "RegularUser",
                        NormalizedName = "RegularUser".ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString("D")
                    };

                    context.Roles.Add(identityRoleUser);

                    context.SaveChanges();
                }
            }
        }
    }
}
