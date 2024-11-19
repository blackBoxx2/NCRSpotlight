
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
                                int randIndex = rnd.Next(suppliers.Count);
                                int supplierID = suppliers[randIndex].ID;

                                Product product = new Product
                                {
                                    ID = id,
                                    Description = prodDescription,
                                    ProductNumber = prodNumber,
                                    SupplierID = supplierID,
                                    Supplier = context.Suppliers.FirstOrDefault(s => s.ID == supplierID),
                                    SapNo = rnd.Next(10000,99999).ToString()
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

                                if(imgCounter > imgPaths.Count()) imgCounter = 0;

                                id++;
                            }
                        }

                        string engID = identityContext.Roles.First(r => r.Name == "Engineer").Id;
                        var eng = await identityContext.UserRoles.Where(u => u.RoleId == engID).Select(u => u.UserId).ToListAsync();

                        Random rand = new Random();

                        if (!context.EngPortions.Any())
                        {
                            for(int i = 0; i < 50; i++) {
                                EngPortion a = new EngPortion();

                                if(rand.NextDouble() >= 0.5)
                                {
                                    int e = rand.Next(eng.Count());

                                    a.RepID = identityContext.Users.FirstOrDefault(u => u.Id == eng[e]).Email;
                                    a.EngReview = (EngReview)rand.Next(1, 3);
                                    a.Notif = rand.NextDouble() >= 0.5;
                                    a.Update = rand.NextDouble() >= 0.5;
                                    a.Disposition = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
                                    a.RevNumber = random.Next(10);
                                    a.OriginalRevNumber = a.RevNumber > 1 ? 1 : 0;
                                    a.RevDate = DateTime.Today.AddDays(-rand.Next(10));
                                    a.Date = a.RevDate.Value.AddDays(rand.Next(10));
                                    a.OriginalEngineer = "None";

                                }

                                context.EngPortions.Add(a);
                            }
                        }



                        //QualityPortion seed data
                        if (!context.QualityPortions.Any())
                        {

                            string[] defect_descs = {
                                "crack on bottom",
                                "sustained damage during shipping",
                                "lcd damaged",
                                "plastic is malformed",
                                "injection mould was not properly filled",
                                "poor threading",
                                "loose connection fit",
                                "frayed wires",
                                "paint is chipped",
                                "varnish is scratched",
                                "deep gouge in left side",
                                "bulb glass came crushed",
                                 "Scratch on surface",
                                "Dents or dings",
                                "Color mismatch",
                                "Missing parts",
                                "Improper assembly",
                                "Defective wiring",
                                "Software bugs",
                                "Leaking components",
                                "Inconsistent sizing",
                                "Poor packaging",
                                "Unresponsive buttons",
                                "Battery failure",
                                "Cracked casing",
                                "Excessive noise",
                                "Incorrect labeling",
                                "Short lifespan",
                                "Staining or discoloration",
                                "Weak structural integrity",
                                "Poor stitching or seams",
                                "Inaccurate measurements",
                                "Overheating issues",
                                "Rust or corrosion",
                                "Misaligned components",
                                "Faulty sensors",
                                "Inadequate insulation",
                                "Bubbles in coating",
                                "Non-compliance with safety standards",
                                "Poorly written instructions",
                                "Inconsistent performance",
                                "Squeaky parts",
                                "Unstable base",
                                "Defective hinges",
                                "Malfunctioning display",
                                "Incorrect voltage",
                                "Poor drainage",
                                "Fading colors",
                                "Defective fasteners",
                                "Noisy operation",
                                "Unexpected shutdowns",
                                "Ineffective features",
                                "Smudged print",
                                "Poor adhesion",
                                "Fragmentation of materials",
                                "Deterioration over time",
                                "Excessive vibrations",
                                "Weak connectivity",
                                "Flashing error lights",
                                "Absence of warranty information",
                                "Unpleasant odors",
                                "Sharp edges",
                                "Faulty locking mechanism",
                                "Incomplete assembly instructions",
                                "Limited functionality",
                                "Difficult to operate"
                            };

                            int ordNum = 1;

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

                            string qaID = identityContext.Roles.First(r => r.Name == "QualityAssurance").Id;
                            var qa = await identityContext.UserRoles.Where(u => u.RoleId == qaID).Select(u => u.UserId).ToListAsync();
                            int imgCounter2 = 0;

                            foreach (string defect in defect_descs)
                            {

                                int a = rand.Next(qa.Count());
                                int quan = rand.Next(1, 1000);

                                if (imgCounter2 == imgPaths.Count()) imgCounter2 = 0;

                                var qp = new QualityPortion
                                {
                                    ProductID = rand.Next(1,productDescriptions.Count),
                                    OrderNumber = $"0001-{ordNum:D4}",
                                    DefectDescription = defect,
                                    Quantity = quan,
                                    QuantityDefective = rand.Next(1, quan),
                                    RepID = identityContext.Users.FirstOrDefault(u => u.Id == qa[a]).Email,
                                    Created = DateTime.Today.AddDays(-(rand.Next(100)))

                                };

                                await WebImagestoByArrayStatic.SeedQualityPictures(imgPaths[imgCounter2], qp);

                                imgCounter2++;

                                context.QualityPortions.Add(qp);
                                ordNum++;

                                await context.SaveChangesAsync();
                            }

                        }

                        if (!context.NCRLog.Any())
                        {
                            Random rando = new Random();
                            
                            for (int i = 1; i <= 50; i++)
                            {
                                var date = new DateTime(2024, rando.Next(1, 12), rando.Next(1, 28));

                                var ncrl = new NCRLog
                                {
                                    EngPortionID = i,
                                    QualityPortionID = i,
                                    DateCreated = date,
                                    Status = NCRStatus.Active,
                                    Phase = NCRPhase.ENG,

                                };
                                if (context.EngPortions.First(e => e.ID == i).RepID != "")
                                {
                                    ncrl.Phase = NCRPhase.PO;
                                }
                                context.NCRLog.Add(ncrl);
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
