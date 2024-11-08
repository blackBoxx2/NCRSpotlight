using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.DataStore.SQLite
{
    public class ApplicationRole:IdentityRole<int>
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base()
        {
            Name = roleName;
        }
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
