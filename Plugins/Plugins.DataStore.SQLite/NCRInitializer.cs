
using EntitiesLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using NCRSPOTLIGHT.Plugins.Plugins.SQLite;
using System.Diagnostics;

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
                    List<string> productPicsList = new List<string>
                    {
                        "https://unsplash.com/photos/person-playing-keyboard-i6VNX0-o7ro",
                        "https://unsplash.com/photos/a-bunch-of-different-types-of-memory-cards-on-a-table-Mg7pduO-CHY",
                        "https://unsplash.com/photos/white-and-green-hard-disk-drive-JMwCe3w7qKk",
                        "https://unsplash.com/photos/person-holding-intel-processor-4Iv0Z1e2nNY",
                        "https://unsplash.com/photos/a-close-up-of-a-computer-screen-with-the-word-geforcertx-on-it-YQf19uMsRgY",
                        "https://unsplash.com/photos/a-close-up-of-a-motherboard-and-a-pen-on-a-table-boMKfQkphro",
                        "https://unsplash.com/photos/gold-iphone-6-on-black-asus-laptop-KcF7fUhKFeg",
                        "https://unsplash.com/photos/a-black-and-yellow-computer-case-on-a-white-background-STqUfdlro-I",
                        "https://unsplash.com/photos/black-computer-tower-on-brown-wooden-table-Z-UuXG6iaA8",
                        "https://unsplash.com/photos/a-close-up-of-a-computer-fan-on-a-table-zt4CPdEiRW4",
                        "https://unsplash.com/photos/black-and-white-corded-computer-mouse-iO0I6-mhDEY",
                        "https://unsplash.com/photos/a-pair-of-headphones-sitting-on-top-of-a-blue-surface-qtBRUc0PADc",
                        "https://unsplash.com/photos/person-holding-silver-and-black-laptop-computer-lYI66oToyCs",
                        "https://unsplash.com/photos/white-router-on-white-table-hXVVNB6Qctg",
                        "https://unsplash.com/photos/black-and-gray-internal-hdd-wYD_wfifJVs",
                        "https://unsplash.com/photos/laptop-computer-beside-monitor-with-keyboard-and-mouse-EJMTKCZ00I0",
                        "https://unsplash.com/photos/a-white-and-black-printer-sitting-on-top-of-a-counter-CGnoRQZGWmw",
                        "https://unsplash.com/photos/black-sony-xperia-on-blue-surface-Ux2j3EAD-_g",
                        "https://unsplash.com/photos/a-keyboard-and-a-mouse-on-a-desk-OkzfCHBVTH8",
                        "https://unsplash.com/photos/person-holding-white-huawei-smartphone-LeVpS-RYGm8"
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

                                await WebImagestoByArrayStatic.SeedProductPictures(productPicsList[imgCounter % productPicsList.Count], product);
                                //string localFilePath = Path.Combine(Environment.CurrentDirectory, $"test_image_{imgCounter}.jpg");
                                //await File.WriteAllBytesAsync(localFilePath, imageBytes);
                                //string fileName = $"image_{product.ID}_{Guid.NewGuid()}.png";
                                //var productPic = new ProductPicture
                                //{
                                //    ProductID = product.ID,
                                //    FileName = fileName,
                                //    MimeType = "image/png",
                                //    FileContent = new FileContent
                                //    {
                                //        Content = imageBytes
                                //    }
                                //};

                                //product.ProductPictures.Add(productPic);
                                context.Products.Add(product);

                                imgCounter++;
                                id++;
                            }
                        }

                        await context.SaveChangesAsync();

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
