using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.DataStore.SQLite
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {

        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
        //public virtual ICollection<ApplicationUser> Users { get; set; }
        //public virtual ICollection<ApplicationId> Roles { get; set; }
    }
}

//IEnumerable<IdentityUserRole<int>>