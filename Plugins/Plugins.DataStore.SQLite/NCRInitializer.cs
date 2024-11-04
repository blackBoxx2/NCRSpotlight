
using EntitiesLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Plugins.DataStore.SQLite
{
    public class NCRInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider,
            bool DeleteDatabase = false, bool UseMigrations = true,
            bool SeedSampleData = true)
        { 
            using (var context = new NCRContext(serviceProvider.GetRequiredService<DbContextOptions<NCRContext>>()))
            {
                #region Prepare the Database
                try
                {
                    if(DeleteDatabase || !context.Database.CanConnect())
                    {
                            context.Database.EnsureDeleted();
                            if(UseMigrations)
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
                            if(UseMigrations)
                            {
                                context.Database.Migrate();
                            }

                     }
                    
                }
                catch(Exception ex)
                { 
                    Debug.WriteLine(ex.GetBaseException().Message);
                }
                #endregion

                #region Start seeding data
                try
                {
                    //Suppliers
                    if (!context.Suppliers.Any())
                    {
                        context.Suppliers.AddRange(
                                new Supplier()
                                {
                                    ID = 1,
                                    SupplierName = "Seed Supplier 1"
                                },
                                new Supplier()
                                {
                                    ID = 2,
                                    SupplierName = "Seed Supplier 2"
                                },
                                new Supplier()
                                {
                                    ID = 3,
                                    SupplierName = "Seed Supplier 3"
                                }
                            );
                        context.SaveChanges();
                    }
                    if (!context.Roles.Any()) 
                    {
                        context.Roles.AddRange(
                            new Role()
                            {
                                ID = 1,
                                RoleName = "Seed Role 1"
                            }
                           );
                        context.SaveChanges();
                    }
                }
                catch
                {

                }
                
                #endregion
            }
        }
    }
}
