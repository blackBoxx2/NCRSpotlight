
using EntitiesLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Plugins.DataStore.SQLite.NCRMigration;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;

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

                #region Start seeding data
                try
                {
                    Random random = new Random();
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
                            if (!context.Suppliers.Any(s => s.SupplierName == supplier))
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
                    //Roles seed data
                    string[] roles = [
                        "QA",
                        "ENG",
                        "Admin"
                    ];
                    int rolesCount = roles.Length;
                    
                        foreach (string role in roles)
                        {

                            Role role1 = new Role()
                            {

                                RoleName = role
                            };

                            try
                            {
                                context.Roles.Add(role1);
                                context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                context.Roles.Remove(role1);
                            }
                        }
                    
                    //Representatives seed data
                    string[] firstNames = [
                        "Alejandro",
                        "Valeria",
                        "Santiago",
                        "Camila",
                        "Diego",
                        "Nathalia",
                        "Luis",
                        "Lorena",
                        "Carlos",
                        "Sofía",
                        "Andrés",
                        "Lucía",
                        "Fernando",
                        "Isabella",
                        "Javier",
                        "Gabriela",
                        "Ricardo",
                        "Mariana",
                        "José",
                        "Karen"];
                    string[] middleNames = ["J.", "L.", "M.", "A.", "R.", "N.", "G.", "S.", "H.", "C.", "D.", "E.", "T.", "B.", "F.", "V.", "Q.", "P.", "K.", "Z."];

                    string[] lastNames = [
                        "Smith",
                        "Johnson",
                        "Williams",
                        "Brown",
                        "Jones",
                        "Garcia",
                        "Miller",
                        "Davis",
                        "Rodriguez",
                        "Martinez",
                        "Hernandez",
                        "Lopez",
                        "Gonzalez",
                        "Wilson",
                        "Anderson",
                        "Taylor",
                        "Thomas",
                        "Moore",
                        "Jackson",
                        "Martin"];
          
                    List<string> SelectedFirst = new List<string>();
                    List<string> SelectedMiddle = new List<string>();
                    List<string> SelectedLast = new List<string>();
                   
                    foreach (string first in firstNames)
                    {
                        SelectedFirst.Add(firstNames[random.Next(firstNames.Length)]);
                        SelectedLast.Add(lastNames[random.Next(lastNames.Length)]);
                        SelectedMiddle.Add(middleNames[random.Next(middleNames.Length)]);
                    }
                    
                    for (int i = 0; i < 20; i++)
                    {
                        Representative representative = new Representative()
                        {
                            FirstName = SelectedFirst[i],
                            MiddleInitial = SelectedMiddle[i],
                            LastName = SelectedLast[i],
                        };
                        try
                        {
                            context.Representatives.Add(representative);
                            context.SaveChanges();

                        }
                        catch (Exception ex)
                        {
                            context.Representatives.Remove(representative);
                        }
                    }


                    //RoleReps Seed Data                   

                    foreach(var ID in context.Representatives.Select(r => r.ID) )
                    {

                        HashSet<int> nums = new HashSet<int>();
                        for (int i = 0; i <= random.Next(0, 3); i++)
                        {
                            nums.Add(random.Next(1, 4));
                        }
                        foreach (int i in nums)
                        {

                            context.RoleReps.AddRange(

                                new RoleRep()
                                {
                                    RoleID = i,
                                    RepresentativeID = ID
                                }

                            );
                            context.SaveChanges();

                        }

                    }
                        
                    
                    //Product Initializer               

                    if (!context.Products.Any())
                    {

                        context.Products.AddRange(
                            new Product()
                            {
                                SupplierID = 1,
                                ProductNumber = "25585",
                                Description = "Bow of faerdhinen",
                                ProductPictures = new HashSet<ProductPicture>(){}
                            },
                            new Product()
                            {
                                SupplierID = 2,
                                ProductNumber = "A45H34a",
                                Description = "Beer",
                                ProductPictures = new HashSet<ProductPicture>(){}
                            },
                            new Product()
                            {
                                SupplierID = 3,
                                ProductNumber = "B567ah",
                                Description = "Ladder",
                                ProductPictures = new HashSet<ProductPicture>() { }
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
