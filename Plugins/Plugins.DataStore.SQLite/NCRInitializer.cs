
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
                    List<string> supplierNames = new List<string>
                    {
                        "Apex Supplies Ltd.",
                        "Blue Ridge Manufacturing Co.",
                        "Central Distributors Inc.",
                        "Dynamic Industrial Solutions",
                        "Eastern Wholesale Partners",
                        "First Choice Suppliers",
                        "Global Imports and Exports",
                        "Horizon Equipment Co.",
                        "Imperial Components Ltd.",
                        "Keystone Distribution Group",
                        "Liberty Supply Corporation",
                        "Maverick Materials LLC",
                        "NorthStar Parts & Tools",
                        "Omega Resource Solutions",
                        "PrimeSource Traders",
                        "Quality Goods International",
                        "Reliable Supply Chain Inc.",
                        "Silverline Services Ltd.",
                        "Titan Manufacturing Group",
                        "United Wholesale Solutions"
                    };
                    int id = 1;
                    //Suppliers
                    if (!context.Suppliers.Any() || context.Suppliers.Count() < 20)
                    {
                        id = context.Suppliers.Any() ? context.Suppliers.Max(s => s.ID) + 1 : 1;
                        foreach (string supplier in supplierNames)
                        {
                            if(!context.Suppliers.Any(s => s.SupplierName == supplier))
                            {
                                context.Suppliers.Add(
                                new Supplier
                                {
                                    ID = id,
                                    SupplierName = supplier
                                }
                                );
                                id++;
                            }
                            else
                            {
                                continue;
                            }                            
                        }
                        context.SaveChanges();
                    }
                    //Representatives
                    if (!context.Representatives.Any())
                    {
                        context.Representatives.AddRange(
                            new Representative()
                            {
                                ID = 1,
                                FirstName = "Josh",
                                MiddleInitial = "P",
                                LastName = "Allen"
                            },
                            new Representative()
                            {
                                ID = 2,
                                FirstName = "Dalton",
                                LastName = "Kincaid"
                            },
                            new Representative()
                            {
                                ID = 3,
                                FirstName = "Keon",
                                MiddleInitial = "A",
                                LastName = "Coleman"
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
                    if (!context.RoleReps.Any())
                    {
                        context.RoleReps.AddRange(
                            new RoleRep()
                            {
                                RoleRepID = 1,
                                RoleID = 1,
                                RepresentativeID = 1
                            },
                            new RoleRep()
                            {
                                RoleRepID = 2,
                                RoleID = 1,
                                RepresentativeID = 2
                            },
                            new RoleRep()
                            {
                                RoleRepID = 3,
                                RoleID = 1,
                                RepresentativeID = 3
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
