using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NCRSPOTLIGHT.Data;
using Plugins.DataStore.SQLite;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.SuppliersUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.SuppliersUseCases;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

builder.Services.AddDbContext<IdentityContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDbContext<NCRContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddDbContext<NCRContext>(options =>
options.UseSqlite(connectionString));

// Add services to the container.
//Without the .AddRazorPages we cant use .cshtml pages
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

//We will come back and add an If statement to check if its in development or QA
builder.Services.AddTransient<ISupplierRepository, SupplierSQLRepository>();

//Register Supplier Services
builder.Services.AddTransient<IAddSupplierAsyncUseCase, AddSupplierAsyncUseCase>();
builder.Services.AddTransient<IDeleteSupplierAsyncUseCase, DeleteSupplierAsyncUseCase>();
builder.Services.AddTransient<IGetSupplierByIDAsyncUseCase, GetSupplierByIdAsyncUseCase>();
builder.Services.AddTransient<IGetSuppliersAsyncUseCase, GetSuppliersAsyncUseCase>();
builder.Services.AddTransient<IUpdateSupplierAsycUseCase, UpdateSupplierAsyncUseCase>();
//Implement Policy Based Authorization (Used for specific roles)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("QARep", p => p.RequireClaim("Role", "QARep"));

    options.AddPolicy("ENRep", p => p.RequireClaim("Role", "ENRep"));

    options.AddPolicy("Admin", p => p.RequireClaim("Role", "Admin"));
    options.AddPolicy("OPManager", p => p.RequireClaim("Role", "OPManager"));
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
    NCRInitializer.Initialize(serviceProvider:services, DeleteDatabase:false,
        UseMigrations:true,SeedSampleData:true);
}
app.Run();
