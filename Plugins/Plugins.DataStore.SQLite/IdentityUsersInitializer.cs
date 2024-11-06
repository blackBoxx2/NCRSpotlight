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

                string adminUserId = Guid.NewGuid().ToString();
                string qaUserId = Guid.NewGuid().ToString();
                string engineerUserId = Guid.NewGuid().ToString();
                string basicUserId = Guid.NewGuid().ToString();
                PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
                var pw = passwordHasher.HashPassword(null, "Adminpassword1");
                if (passwordHasher.VerifyHashedPassword(null, pw, "Adminpassword1") != PasswordVerificationResult.Success)
                {
                    throw new Exception("Password hashing failed");
                }
                if (!context.Roles.Any())
                {
                    context.Roles
                        .AddRange(
                        new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                        new IdentityRole { Id = qaRoleId, Name = "QualityAssurance", NormalizedName = "QUALITYASSURANCE" },
                        new IdentityRole { Id = engineerRoleId, Name = "Engineer", NormalizedName = "ENGINEER" },
                        new IdentityRole { Id = basicUserRoleId, Name = "BasicUser", NormalizedName = "BASICUSER" }
                        );
                    context.SaveChanges();
                }


                if (!context.Users.Any())
                {
                    context.Users
                    .AddRange(
                    new IdentityUser
                    {
                        Id = adminUserId,
                        UserName = "admin@email.com",
                        NormalizedUserName = "ADMIN@EMAIL.COM",
                        Email = "admin@email.com",
                        NormalizedEmail = "ADMIN@EMAIL.COM",
                        EmailConfirmed = true,
                        PasswordHash = passwordHasher.HashPassword(null, "Adminpassword1.")
                    },
                     new IdentityUser
                     {
                         Id = qaUserId,
                         UserName = "qa@email.com",
                         NormalizedUserName = "QA@EMAIL.COM",
                         Email = "qa@email.com",
                         NormalizedEmail = "QA@EMAIL.COM",
                         EmailConfirmed = true,
                         PasswordHash = passwordHasher.HashPassword(null, "Qapassword1.")
                     },
                     new IdentityUser
                     {
                         Id = engineerUserId,
                         UserName = "engineer@email.com",
                         NormalizedUserName = "ENGINEER@EMAIL.COM",
                         Email = "engineer@email.com",
                         NormalizedEmail = "ENGINEER@EMAIL.COM",
                         EmailConfirmed = true,
                         PasswordHash = passwordHasher.HashPassword(null, "Engineerpassword1.")
                     },
                     new IdentityUser
                     {
                         Id = basicUserId,
                         UserName = "basic@email.com",
                         NormalizedUserName = "BASIC@EMAIL.COM",
                         Email = "basic@email.com",
                         NormalizedEmail = "BASIC@EMAIL.COM",
                         EmailConfirmed = true,
                         PasswordHash = passwordHasher.HashPassword(null, "Basicpassword1.")
                     }
                    );
                    context.SaveChanges();
                }

                //give these folks some roles!
                if (!context.UserRoles.Any())
                {
                    context.UserRoles
                    .AddRange(
                    new IdentityUserRole<string> { UserId = adminUserId, RoleId = adminRoleId },
                    new IdentityUserRole<string> { UserId = qaUserId, RoleId = qaRoleId },
                    new IdentityUserRole<string> { UserId = engineerUserId, RoleId = engineerRoleId },
                    new IdentityUserRole<string> { UserId = basicUserId, RoleId = basicUserRoleId }
                    );

                    context.SaveChanges();
                }
            }
        }
    }


}
