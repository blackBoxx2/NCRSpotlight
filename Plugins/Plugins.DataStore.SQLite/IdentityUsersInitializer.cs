using EntitiesLayer.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Plugins.DataStore.SQLite
{
    public class IdentityUsersInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider,
            bool DeleteDatabase = true, bool UseMigrations = true,
            bool SeedSampleData = true)
        {
            using (var context = new IdentityContext(serviceProvider.GetRequiredService<DbContextOptions<IdentityContext>>()))
            {
                #region Prepare the Database
                try
                {
                    if (DeleteDatabase || !context.Database.CanConnect())
                    {
                        context.Database.EnsureDeleted();
                        if (UseMigrations)
                        {
                            context.Database.Migrate();
                        }
                        else
                        {
                            context.Database.EnsureCreated();
                        }

                        //Will return to add Auditing
                    }
                    else
                    {
                        if (UseMigrations)
                        {
                            context.Database.Migrate();
                        }

                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.GetBaseException().Message);
                }
                #endregion

                string adminRoleId = Guid.NewGuid().ToString();
                string qaRoleId = Guid.NewGuid().ToString();
                string engineerRoleId = Guid.NewGuid().ToString();
                string basicUserRoleId = Guid.NewGuid().ToString();
                string superAdminRoleId = Guid.NewGuid().ToString();

                string adminUserId = Guid.NewGuid().ToString();
                string qaUserId = Guid.NewGuid().ToString();
                string engineerUserId = Guid.NewGuid().ToString();
                string basicUserId = Guid.NewGuid().ToString();
                string superAdminId = Guid.NewGuid().ToString();

                PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

                if (!context.Roles.Any())
                {
                    context.Roles
                        .AddRange(
                        new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                        new IdentityRole { Id = qaRoleId, Name = "QualityAssurance", NormalizedName = "QUALITYASSURANCE" },
                        new IdentityRole { Id = engineerRoleId, Name = "Engineer", NormalizedName = "ENGINEER" },
                        new IdentityRole { Id = basicUserRoleId, Name = "BasicUser", NormalizedName = "BASICUSER" },
                        new IdentityRole { Id = superAdminId, Name = "SuperAdmin", NormalizedName = "SUPERADMIN" }
                        );
                    context.SaveChanges();
                }


                if (!context.Users.Any())
                {
                    context.Users
                    .AddRange(
                    new ApplicationUser
                    {
                        Id = adminUserId,
                        UserName = "admin@email.com",
                        NormalizedUserName = "ADMIN@EMAIL.COM",
                        Email = "admin@email.com",
                        NormalizedEmail = "ADMIN@EMAIL.COM",
                        EmailConfirmed = true,
                        PasswordHash = passwordHasher.HashPassword(null, "password"),
                        RoleId = adminRoleId,
                    },
                     new ApplicationUser
                     {
                         Id = qaUserId,
                         UserName = "qa@email.com",
                         NormalizedUserName = "QA@EMAIL.COM",
                         Email = "qa@email.com",
                         NormalizedEmail = "QA@EMAIL.COM",
                         EmailConfirmed = true,
                         PasswordHash = passwordHasher.HashPassword(null, "password"),
                         RoleId = qaRoleId
                     },
                     new ApplicationUser
                     {
                         Id = engineerUserId,
                         UserName = "engineer@email.com",
                         NormalizedUserName = "ENGINEER@EMAIL.COM",
                         Email = "engineer@email.com",
                         NormalizedEmail = "ENGINEER@EMAIL.COM",
                         EmailConfirmed = true,
                         PasswordHash = passwordHasher.HashPassword(null, "password"),
                         RoleId = engineerRoleId
                     },
                     new ApplicationUser
                     {
                         Id = basicUserId,
                         UserName = "basic@email.com",
                         NormalizedUserName = "BASIC@EMAIL.COM",
                         Email = "basic@email.com",
                         NormalizedEmail = "BASIC@EMAIL.COM",
                         EmailConfirmed = true,
                         PasswordHash = passwordHasher.HashPassword(null, "password"),
                         RoleId = basicUserRoleId
                     },
                     new ApplicationUser
                     {
                         Id = superAdminId,
                         UserName = "superAdmin@email.com",
                         NormalizedUserName = "SUPERADMIN@EMAIL.COM",
                         Email = "superAdmin@email.com",
                         NormalizedEmail = "SUPERADMIN@EMAIL.COM",
                         EmailConfirmed = true,
                         PasswordHash = passwordHasher.HashPassword(null, "password"),
                         RoleId = basicUserRoleId
                     }
                    );
                    context.SaveChanges();
                }

                //give these folks some roles!
                //if (!context.UserRoles.Any())
                //{
                //    context.UserRoles
                //    .AddRange(
                //    new IdentityUserRole<string> { UserId = adminUserId, RoleId = adminRoleId },
                //    new IdentityUserRole<string> { UserId = qaUserId, RoleId = qaRoleId },
                //    new IdentityUserRole<string> { UserId = engineerUserId, RoleId = engineerRoleId },
                //    new IdentityUserRole<string> { UserId = basicUserId, RoleId = basicUserRoleId }
                //    );

                //    context.SaveChanges();
                //}
            }
        }
    }


}
