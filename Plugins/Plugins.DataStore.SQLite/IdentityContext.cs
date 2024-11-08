using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace Plugins.DataStore.SQLite;

public class IdentityContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite("Filename=NCRSPOTLIGHTIdentity.db");
            optionsBuilder.AddInterceptors(new EnableForeignKeysInterceptor());
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);


    }

    public class EnableForeignKeysInterceptor : DbCommandInterceptor
    {
        public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(
            DbCommand command,
            CommandEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            command.CommandText = "PRAGMA foreign_keys = ON;" + command.CommandText;
            return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
        }
    }
}
