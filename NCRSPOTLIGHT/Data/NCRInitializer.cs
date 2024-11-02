
using Microsoft.EntityFrameworkCore;

namespace NCRSPOTLIGHT.Data
{
    public class NCRInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider,
            bool DeleteDatabase = false, bool UseMigrations = true,
            bool SeedSampleData = false)
        { 
            using (var context = new NCRContext(serviceProvider.GetRequiredService<DbContextOptions<NCRContext>>()))
            {

            }
        }
    }
}
