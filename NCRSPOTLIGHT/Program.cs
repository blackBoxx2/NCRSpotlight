using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using NCRSPOTLIGHT.Areas.Identity.Data;
using NCRSPOTLIGHT.Utilities;
using Plugins.DataStore.SQLite;
using Plugins.DataStore.SQLite.JoinsUseCase;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.NCRLogUseCase;
using UseCasesLayer.UseCaseInterfaces.NCRLogUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.ProductUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.ProductUseCases;
using UseCasesLayer.UseCaseInterfaces.QualityPortionUseCase;
using UseCasesLayer.UseCaseInterfaces.QualityPortionUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.RepresentativesUseCase;
using UseCasesLayer.UseCaseInterfaces.RepresentitiveUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.RepresentitvesUseCase;
using UseCasesLayer.UseCaseInterfaces.RoleRepUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.RoleRepUseCases;
using UseCasesLayer.UseCaseInterfaces.RoleUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.RoleUseCases;
using UseCasesLayer.UseCaseInterfaces.SuppliersUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.SuppliersUseCases;
var builder = WebApplication.CreateBuilder(args);
var connectionStringNCRSpotlight = builder.Configuration.GetConnectionString("NCRContext") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

var connectionStringIdentity = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");
#region Emailer Stuff
builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
        .AddJsonFile("Secrets/secrets.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();

var smtpUsername = builder.Configuration["Smtp:Username"];
var smtpPassword = builder.Configuration["Smtp:Password"];
var smtpHost = builder.Configuration["Smtp:Host"];
var smtpPort = builder.Configuration["Smtp:Port"];
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

builder.Services.AddTransient<IEmailSender, NCRSpotlightEmailer>();
#endregion
builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlite(connectionStringIdentity));

builder.Services.AddDbContext<NCRContext>(options => options.UseSqlite(connectionStringNCRSpotlight));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
}).AddEntityFrameworkStores<IdentityContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<QualityPortionService>();

// Add services to the container.
//Without the .AddRazorPages we cant use .cshtml pages
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

//We will come back and add an If statement to check if its in development or QA
builder.Services.AddTransient<ISupplierRepository, SupplierSQLRepository>();
builder.Services.AddTransient<IRoleRepository, RoleSQLRepository>();
builder.Services.AddTransient<IRepresentativeRepository, RepresentativeSQLRepository>();
builder.Services.AddTransient<IRoleRepRepository, RoleRepSQLRepository>();
builder.Services.AddTransient<IProductRepository, ProductSQLRepository>();
builder.Services.AddTransient<IQualityPortionSQLRepository, QualityPortionSQLRepository>();
builder.Services.AddTransient<INCRLogRepository, NCRLogSQLRepository>();


#region Register Supplier Services
//Supplier
builder.Services.AddTransient<IAddSupplierAsyncUseCase, AddSupplierAsyncUseCase>();
builder.Services.AddTransient<IDeleteSupplierAsyncUseCase, DeleteSupplierAsyncUseCase>();
builder.Services.AddTransient<IGetSupplierByIDAsyncUseCase, GetSupplierByIdAsyncUseCase>();
builder.Services.AddTransient<IGetSuppliersAsyncUseCase, GetSuppliersAsyncUseCase>();
builder.Services.AddTransient<IUpdateSupplierAsyncUseCase, UpdateSupplierAsyncUseCase>();

//Representative
builder.Services.AddTransient<IAddRepresentativeAsyncUseCase, AddRepresentativeAsyncUseCase>();
builder.Services.AddTransient<IDeleteRepresentativeAsyncUseCase, DeleteRepresentativeAsyncUseCase>();
builder.Services.AddTransient<IGetRepresentativesByIdAsyncUseCase, GetRepresentativesByIdAsyncUseCase>();
builder.Services.AddTransient<IGetRepresentativesAsyncUseCase, GetRepresentativesAsyncUseCase>();
builder.Services.AddTransient<IUpdateRepresentativeAsyncUseCase, UpdateRepresentativeAsyncUseCase>();

//Register Role Services
builder.Services.AddTransient<IAddRoleAsyncUserCase, AddRoleAsyncUseCase>();
builder.Services.AddTransient<IDeleteRoleAsyncUserCase, DeleteRoleAsyncUseCase>();
builder.Services.AddTransient<IGetRoleByIDAsyncUserCase, GetRolByIDUseCase>();
builder.Services.AddTransient<IGetRoleAsyncUserCase, GetRoleAsyncUseCase>();
builder.Services.AddTransient<IUpdateRoleAsyncUserCase, UpdateRoleAsyncUseCase>();

//RoleRep
builder.Services.AddTransient<IAddRoleRepAsyncUseCase, AddRoleRepAsyncUseCase>();
builder.Services.AddTransient<IDeleteRoleRepAsyncUseCase, DeleteRoleRepAsyncUseCase>();
builder.Services.AddTransient<IGetRoleRepByIDAsyncUseCase, GetRoleRepByIdAsyncUseCase>();
builder.Services.AddTransient<IGetRoleRepAsyncUseCase, GetRoleRepAsyncUseCase>();
builder.Services.AddTransient<IUpdateRoleRepAsyncUseCase, UpdateRoleRepAsyncUseCase>();

//product
builder.Services.AddTransient<IAddProductAsyncUseCase, AddProductAsyncUseCase>();
builder.Services.AddTransient<IDeleteProductAsyncUseCase, DeleteProductAsyncUseCase>();
builder.Services.AddTransient<IUpdateProductAsyncUseCase, UpdateProductAsyncUseCase>();
builder.Services.AddTransient<IGetProductByIDAsyncUseCase, GetProductByIDAsyncUseCase>();
builder.Services.AddTransient<IGetProductsAsyncUseCase, GetProductsAsyncUseCase>();

//QualityPortion
builder.Services.AddTransient<IAddQualityPortionAsyncUseCase, AddQualityPortionAsyncUseCase>();
builder.Services.AddTransient<IDeleteQualityPortionAsyncUseCase, DeleteQualityPortionAsyncUseCase>();
builder.Services.AddTransient<IUpdateQualityPortionAsyncUseCase, UpdateQualityPortionAsyncUseCase>();
builder.Services.AddTransient<IGetQualityPortionByIDAsyncUseCase, GetQualityPortionByIDAsyncUseCase>();
builder.Services.AddTransient<IGetQualityPortionsAsyncUseCase, GetQualityPortionsAsyncUseCase>();

//NCRLog
builder.Services.AddTransient<IAddNCRLogAsyncUseCase, AddNCRLogAsyncUseCase>();
builder.Services.AddTransient<IDeleteNCRLogAsyncUseCase, DeleteNCRLogAsyncUseCase>();
builder.Services.AddTransient<IUpdateNCRLogAsyncUseCase, UpdateNCRLogAsyncUseCase>();
builder.Services.AddTransient<IGetNCRLogByIDAsyncUseCase, GetNCRLogByIDAsyncUseCase>();
builder.Services.AddTransient<IGetNCRLogsAsyncUseCase, GetNCRLogsAsyncUseCase>();

#endregion


//Implement Policy Based Authorization (Used for specific roles)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BasicUser", p => p.RequireRole("BasicUser"));


    options.AddPolicy("Engineer", p => p.RequireRole("Engineer"));
    //    options.AddPolicy("ENRep", p => p.RequireClaim("Role", "ENRep"));

    options.AddPolicy("Admin", p => p.RequireRole("Admin"));
    options.AddPolicy("QualityAssurance", p => p.RequireRole("QualityAssurance"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Middleware for Identity Authorization and Authentication added, please do not delete
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseRouting();
//Razor pages support
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentityUsersInitializer.InitializeAsync(serviceProvider: services, DeleteDatabase: true,
        UseMigrations: true, SeedSampleData: true);
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    NCRInitializer.Initialize(serviceProvider:services, DeleteDatabase:true,
        UseMigrations:true,SeedSampleData:true);
}







app.Run();
