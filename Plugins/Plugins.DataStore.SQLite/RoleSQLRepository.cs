using EntitiesLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.DataStore.SQLite
{
    public class RoleSQLRepository : IRoleRepository
    {
        private readonly NCRContext _context;

        public RoleSQLRepository(NCRContext context)
        {
            _context = context;
        }

        // List the roles
        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            var roles = await _context.Roles
                .Include(g => g.RoleReps)
                .AsNoTracking()
                .ToListAsync();
            return roles;
        }

        public async Task<Role> GetRoleByIDAsync(int? ID)
        {
            if (ID == null) { return null; }
            var role = await _context.Roles
                .Include(g => g.RoleReps)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.ID == ID);
            return role;

        }
        //Add the role

        public async Task AddRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        //Edit a Role
        public async Task UpdateRoleAsync(int? id, Role role)
        {
            if (id != role.ID) { return; }
            var RoleToUpdate = await _context.Roles
                .Include(g => g.RoleReps)
                .FirstOrDefaultAsync(g => g.ID == id);
            if (role == null) { return; }

            RoleToUpdate.RoleName = role.RoleName;
            await _context.SaveChangesAsync();
        }
        //Delete a role 

        public async Task DeleteRoleAsync(int id)
        {
            var role = await _context.Roles
                .FirstOrDefaultAsync(g => g.ID == id);
            if (role != null)
            {
                if (role.RoleReps.Count() > 0)
                {
                    throw new InvalidOperationException("Impossible to delete a role that as a role rep in the system");
                }
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

    }
}
