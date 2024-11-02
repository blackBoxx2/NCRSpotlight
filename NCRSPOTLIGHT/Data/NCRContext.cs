using Microsoft.EntityFrameworkCore;

namespace NCRSPOTLIGHT.Data
{
    public class NCRContext:DbContext
    {
        public NCRContext(DbContextOptions<NCRContext> options)
            :base(options)
        {
            
        }


    }
}
