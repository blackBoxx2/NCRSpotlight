using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Plugins.DataStore.SQLite
{
    public class ApplicationUser:IdentityUser<int>
    {

        public ApplicationUser() : base() { }

        public ApplicationUser(string userName) : this()
        {
            UserName = userName;
        }
        public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
