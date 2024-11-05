
using EntitiesLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Plugins.DataStore.SQLite.NCRMigration;
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
                    string[] roles = new string[]
                    {
                        "Project Manager",
                        "Data Analyst",
                        "Customer Service Representative",
                        "Marketing Specialist",
                        "Software Developer",
                        "Quality Assurance Tester",
                        "Financial Analyst",
                        "Human Resources Coordinator",
                        "Operations Manager",
                        "Graphic Designer",
                        "Account Executive",
                        "Sales Associate",
                        "Business Consultant",
                        "Content Writer",
                        "Technical Support Specialist",
                        "Logistics Coordinator",
                        "Product Manager",
                        "Accountant",
                        "Web Developer",
                        "Executive Assistant"
                    };
                    int rolesCount = roles.Length;
                     if (context.Roles.Count() < 5)
                     {
                        foreach(string role in roles) 
                        {
                           
                            HashSet<string> SelectedRole = new HashSet<string>();
                            while(SelectedRole.Count() > 1) 
                            {
                                SelectedRole.Add(roles[random.Next(rolesCount)]);
                            }
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
                    //Representatives seed data
                    string[] firstNames = new string[]
                        { "Alejandro",
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
                        "Karen"};
                    string[] middleNames = new string[]
                    { "J.", "L.", "M.", "A.", "R.", "N.", "G.", "S.", "H.", "C.", "D.", "E.", "T.", "B.", "F.", "V.", "Q.", "P.", "K.", "Z."};

                    string[] lastNames = new string[]
                        {"Smith",
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
                        "Martin"};
                    int firstCount = firstNames.Length;
                    int middleCount = middleNames.Length;
                    int lastCount = lastNames.Length;
                    if (context.Representatives.Count() < 5)
                    {
                    
                        foreach (string first in firstNames)
                        {
                            
                            HashSet<string> SelectedMiddle = new HashSet<string>();
                            HashSet<string> SelectedLast = new HashSet<string>();
                            while (SelectedLast.Count() < 1)
                            {
                                SelectedLast.Add(lastNames[random.Next(lastCount)]);
                            }
                            while (SelectedMiddle.Count() < 1)
                            {
                               SelectedMiddle.Add(middleNames[random.Next(middleCount)]);
                            }
                            foreach (string middle in SelectedMiddle)
                            {

                                foreach (string last in SelectedLast)
                                {
                                    Representative representative = new Representative()
                                    {
                                        FirstName = first,
                                        MiddleInitial = middle,
                                        LastName = last,
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
                        }
                    }
                    //RoleReps Seed Data
                    int[] RoleRepIDs = context.RoleReps.Select(g => g.RoleRepID).ToArray();
                    int[] RoleIDs = context.Roles.Select(g => g.ID).ToArray();
                    int[] RepresentativeIDs = context.Representatives.Select(g => g.ID).ToArray();
                    int RoleRepIDCount = RoleRepIDs.Length;
                    int RoleIDCount = RoleIDs.Length;
                    int RepresentativeIDCount = RepresentativeIDs.Length;
                    if (context.Representatives.Count() > 5)
                    {
                        foreach (int role in RoleIDs) 
                        {
                            HashSet<int> SelectedRepresentative = new HashSet<int>();
                            while(SelectedRepresentative.Count() < 1) 
                            {
<<<<<<< HEAD
                                SelectedRepresentative.Add(random.Next(RepresentativeIDCount));
=======
                                ID = 1,
                                RoleName = "QA"
>>>>>>> 8c1359328423186a3a2e58edb737a779226f6490
                            }
                            foreach(int representativeID in SelectedRepresentative) 
                            {
                                RoleRep roleRep = new RoleRep()
                                {
                                    RoleID = role,
                                    RepresentativeID = representativeID
                            };
                                try
                                {
                                    context.RoleReps.Add(roleRep);
                                    context.SaveChanges();
                                }
                                catch (Exception ) 
                                {
                                    context.RoleReps.Remove(roleRep);
                                }
                            }
                        }
                    }
                    //Product Initializer
                    string[] productNumbers = new string[] 
                    {
                        "Wireless Earbuds",
                        "Smartphone Charger",
                        "Laptop Stand",
                        "Bluetooth Speaker",
                        "Portable Power Bank",
                        "Stainless Steel Water Bottle",
                        "Electric Kettle",
                        "LED Desk Lamp",
                        "Noise-Canceling Headphones",
                        "Fitness Tracker",
                        "Travel Backpack",
                        "USB Flash Drive",
                        "Wireless Mouse",
                        "Smart Home Hub",
                        "Insulated Coffee Mug",
                        "Portable Projector",
                        "Smartwatch",
                        "Electric Toothbrush",
                        "Air Purifier",
                        "Digital Thermometer"
                    };
                    string[] productDescription = new string[] 
                    {
                        "Wireless Earbuds: Compact, wireless in-ear headphones with Bluetooth connectivity.",
                        "Smartphone Charger: A portable device used to recharge smartphone batteries.",
                        "Laptop Stand: A stand to elevate laptops for better ergonomics and cooling.",
                        "Bluetooth Speaker: A portable speaker that connects via Bluetooth for wireless audio.",
                        "Portable Power Bank: A rechargeable battery pack to charge devices on the go.",
                        "Stainless Steel Water Bottle: Durable water bottle that keeps beverages cold or hot.",
                        "Electric Kettle: A small appliance for quickly boiling water.",
                        "LED Desk Lamp: An energy-efficient desk light with adjustable brightness.",
                        "Noise-Canceling Headphones: Headphones that reduce background noise for clear audio.",
                        "Fitness Tracker: A wearable device that monitors health metrics like steps and heart rate.",
                        "Travel Backpack: A versatile bag with compartments for travel essentials.",
                        "USB Flash Drive: A small portable storage device for transferring files.",
                        "Wireless Mouse: A computer mouse that connects via Bluetooth or USB receiver.",
                        "Smart Home Hub: A central device to control other smart home devices.",
                        "Insulated Coffee Mug: A mug designed to keep beverages at a steady temperature.",
                        "Portable Projector: A compact projector for on-the-go presentations or movie nights.",
                        "Smartwatch: A wearable device that connects to your smartphone for notifications and apps.",
                        "Electric Toothbrush: A toothbrush with automated bristle movement for efficient cleaning.",
                        "Air Purifier: A device that filters and cleans indoor air for better quality.",
                        "Digital Thermometer: A quick and accurate electronic temperature-measuring device."
                    };
                    int productNumbersCount = productNumbers.Length;
                    int productDescriptionCount = productDescription.Length;
                    int[] SuplliersID = context.Suppliers.Select(g => g.ID).ToArray();
                    int SupplierIDsCount = SuplliersID.Length;
                    if (context.Products.Count() > 5) 
                    {
                        foreach(string number in productNumbers) 
                        {
                            HashSet<string> selectedDescription = new HashSet<string>();
                            HashSet<int> SelectedSupplier = new HashSet<int>();
                            while(selectedDescription.Count() < 1) 
                            {
                                selectedDescription.Add(productDescription[random.Next(productDescriptionCount)]);
                            }
                            while(SelectedSupplier.Count() < 1) 
                            {
                                SelectedSupplier.Add(random.Next(SupplierIDsCount));
                            }
                            foreach (string representativeDescription in selectedDescription)
                            {
                                foreach(int SupplierId in SelectedSupplier) 
                                {
                                    Product product = new Product() 
                                    {
                                        ProductNumber = number,
                                        Description = representativeDescription,
                                        SupplierID = SupplierId,
                                    };
                                    try 
                                    {
                                        context.Products.Add(product);
                                        context.SaveChanges();
                                    }
                                    catch(Exception) 
                                    {
                                        context.Remove(product);
                                    }
                                }
                            }
                            
                        }
                    }
                    


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
