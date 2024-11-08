
using EntitiesLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Diagnostics;
using Plugins.DataStore.SQLite.Utilities;

namespace Plugins.DataStore.SQLite
{
    public class NCRInitializer
    {

        

        public static async void Initialize(IServiceProvider serviceProvider,
            bool DeleteDatabase = false, bool UseMigrations = true,
            bool SeedSampleData = true)
        {
            using (var context = new NCRContext(serviceProvider.GetRequiredService<DbContextOptions<NCRContext>>()))
            {
                using var identityContext = new IdentityContext(serviceProvider.GetRequiredService<DbContextOptions<IdentityContext>>());

                


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
                    var identityRoles = await identityContext.Roles.Select(r => r.Name).ToListAsync();
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
                    #region Product Seed Data Lists
                    List<string> productDescriptions = new List<string>
                    {
                        "SteelSeries Apex Pro Mechanical Keyboard",
                        "Corsair Vengeance RGB Pro DDR4 RAM",
                        "AMD Ryzen 9 5900X Processor",
                        "Intel Core i9-12900K Processor",
                        "NVIDIA GeForce RTX 3080 Ti Graphics Card",
                        "Samsung 980 Pro NVMe SSD",
                        "ASUS ROG Strix Z690 Motherboard",
                        "MSI MPG Gungnir 110R Mid-Tower Case",
                        "EVGA SuperNOVA 850W Power Supply",
                        "Cooler Master Hyper 212 RGB Cooler",
                        "Logitech G502 HERO Gaming Mouse",
                        "Razer Kraken Ultimate Gaming Headset",
                        "Kingston A2000 M.2 NVMe SSD",
                        "TP-Link Archer AX6000 WiFi 6 Router",
                        "Seagate IronWolf 8TB NAS Hard Drive",
                        "Dell Ultrasharp 27 4K Monitor",
                        "HP LaserJet Pro M404dn Printer",
                        "Synology DS920+ NAS Storage",
                        "Elgato Stream Deck XL",
                        "Anker PowerCore 26800 Portable Charger"
                    };

                    List<string> productNumbers = new List<string>
                    {
                        "A0-0001",
                        "A0-0002",
                        "A0-0003",
                        "A0-0004",
                        "A0-0005",
                        "A0-0006",
                        "A0-0007",
                        "A0-0008",
                        "A0-0009",
                        "A0-0010",
                        "A0-0011",
                        "A0-0012",
                        "A0-0013",
                        "A0-0014",
                        "A0-0015",
                        "A0-0016",
                        "A0-0017",
                        "A0-0018",
                        "A0-0019",
                        "A0-0020"
                    };

                    #endregion

                        //Suppliers
                    if (!context.Suppliers.Any() || context.Suppliers.Count() < 20)
                    {
                        int id = context.Suppliers.Any() ? context.Suppliers.Max(s => s.ID) + 1 : 1;
                        foreach (string supplierName in supplierNames)
                        {
                            if(!context.Suppliers.Any(s => s.SupplierName == supplierName))
                            {
                                context.Suppliers.Add(
                                new Supplier
                                {
                                    ID = id,
                                    SupplierName = supplierName,
                                    
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
                    //string[] roles = ["Admin", "QA"];


                    //int rolesCount = identityRoles.Count;
                    if(!context.Roles.Any())
                    {
                        foreach (string role in identityRoles)
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
                    }
                        
                    if(!context.Representatives.Any())
                    {
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
                    }



                    //RoleReps Seed Data                   
                    if (!context.RoleReps.Any()) 
                    {
                        foreach (var ID in context.Representatives.Select(r => r.ID))
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

                            }

                        }
 
                            await context.SaveChangesAsync();
                    }
                    
                        
                    
                    //Product Initializer               

                    if (!context.Products.Any() || context.Products.Count() < 20)
                    {
                        Random rnd = new Random();
                        int id = context.Products.Any() ? context.Products.Max(s => s.ID) + 1 : 1;

                        int maxCount = Math.Min(productDescriptions.Count, productNumbers.Count);
                        int imgCounter = 0;

                        for (int i = 0; i < maxCount; i++)
                        {
                            string prodDescription = productDescriptions[i];
                            string prodNumber = productNumbers[i];

                            if (!context.Products.Any(p => p.Description == prodDescription))
                            {

                                var suppliers = context.Suppliers.ToList();
                                int randIndex = rnd.Next( suppliers.Count );
                                int supplierID = suppliers[randIndex].ID;

                                Product product = new Product
                                {
                                    ID = id,
                                    Description = prodDescription,
                                    ProductNumber = prodNumber,
                                    SupplierID = supplierID,
                                    Supplier = context.Suppliers.FirstOrDefault(s => s.ID == supplierID),
                                };

                                List<string> imgPaths = new List<string>()
                                {
                                    @"Assets\ProductImages\keyboard.jpg",
                                    @"Assets\ProductImages\ram.jpg",
                                    @"Assets\ProductImages\ryzen.jpg",
                                    @"Assets\ProductImages\intel.jpg",
                                    @"Assets\ProductImages\nvidia.jpg",
                                    @"Assets\ProductImages\ssd.jpg",
                                    @"Assets\ProductImages\rog-motherboard.jpg",
                                    @"Assets\ProductImages\towercase.jpg",
                                    @"Assets\ProductImages\powersupply.jpg",
                                    @"Assets\ProductImages\pccooler.jpg",
                                    @"Assets\ProductImages\mouse.jpg",
                                    @"Assets\ProductImages\headset.jpg",
                                    @"Assets\ProductImages\kingstonssd.jpg",
                                    @"Assets\ProductImages\router.jpg",
                                    @"Assets\ProductImages\hdd.jpg",
                                    @"Assets\ProductImages\monitor.jpg",
                                    @"Assets\ProductImages\printer.jpg",
                                    @"Assets\ProductImages\NAS.jpg",
                                    @"Assets\ProductImages\steamdeck.jpg",
                                    @"Assets\ProductImages\portablecharger.jpg"
                                };

                                await WebImagestoByArrayStatic.SeedProductPictures(imgPaths[imgCounter], product);

                                context.Products.Add(product);
                                imgCounter++;
                                id++;
                            }
                        }

                        await context.SaveChangesAsync();

                    }
                }
                

                catch(Exception ex)
                {
                    Debug.WriteLine($"Error here: {ex.Message}");
                }

                
                #endregion
            }
            
        }
    
        
    }

    
}
