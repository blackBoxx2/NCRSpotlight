using EntitiesLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQLite
{
    public class RoleSQLRepository : IRoleRepository
    {
        private readonly NCRContext _context;
        private readonly IdentityContext identityContext;

        public RoleSQLRepository(NCRContext context, IdentityContext identityContext)
        {
            _context = context;
            this.identityContext = identityContext;
        }

        // List the roles
        public async Task<IEnumerable<IdentityRole>> GetRolesAsync()
        {
            var roles = await identityContext.Roles
                .ToListAsync();
            return roles;
        }

        public async Task<IdentityRole> GetRoleByIDAsync(string Id)
        {
            if (Id == null) { return null; }
            var role = await identityContext.Roles
                .FirstOrDefaultAsync(g => g.Id == Id);
            return role;

        }
        //Add the role

        public async Task AddRoleAsync(IdentityRole role)
        {
            identityContext.Roles.Add(role);
            await identityContext.SaveChangesAsync();
        }

        //Edit a Role
        public async Task UpdateRoleAsync(string id, IdentityRole role)
        {
            if (id != role.Id) { return; }
            var RoleToUpdate = await identityContext.Roles
                .FirstOrDefaultAsync(g => g.Id == id);
            if (role == null) { return; }

            RoleToUpdate.Name = role.Name;
            await identityContext.SaveChangesAsync();
        }
        //Delete a role 

        public async Task DeleteRoleAsync(string id)
        {
            var role = await identityContext.Roles
                .FirstOrDefaultAsync(g => g.Id == id);
            if (role != null)
            {
                var userRole = await identityContext.UserRoles.FindAsync(id);
                if ( userRole != null)
                {
                    throw new InvalidOperationException("Impossible to delete a role that as a role rep in the system");
                }
                identityContext.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

    }
}
