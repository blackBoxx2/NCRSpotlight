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
    public class RoleRepSQLRepository : IRoleRepRepository
    {
        private readonly NCRContext ncrContext;
        private readonly IdentityContext identityContext;

        public RoleRepSQLRepository(NCRContext ncrContext, IdentityContext identityContext)
        {
            ncrContext = ncrContext;
            this.identityContext = identityContext;
        }

        // list all roleRep
        public async Task<IEnumerable<IdentityUserRole<string>>> GetRoleRepASync()
        {
            var roleReps = await identityContext.UserRoles
                .ToListAsync();
            return roleReps;
        }

       
        public async Task<IdentityUserRole<string>> GetRoleRepByIdAsync(string id)
        {
            if (id == null) return null;

            var roleRep = await identityContext.UserRoles
                .FirstOrDefaultAsync(m => m.RoleId == id);

            return roleRep;
        }

        // add a roleRep
        public async Task AddRoleRepAsync(IdentityUserRole<string> roleRep)
        {
            identityContext.UserRoles.Add(roleRep);
            await identityContext.SaveChangesAsync();
        }

        // edit roleRep
        public async Task UpdateRoleRepAsync(string id, IdentityUserRole<string> roleRep)
        {
            if (id != roleRep.RoleId) return;

            var roleRepToUpdate = await identityContext.UserRoles
                                        .FirstOrDefaultAsync(r => r.RoleId == id);
            if (roleRepToUpdate == null) return;

            roleRepToUpdate.RoleId = roleRep.RoleId;
            roleRepToUpdate.UserId = roleRep.UserId;
            await identityContext.SaveChangesAsync();
        }

        // delete roleRep
        public async Task DeleteRoleRepAsync(string id)
        {
            var roleRep = await identityContext.UserRoles
                                .FirstOrDefaultAsync(r => r.RoleId == id);
            if (roleRep != null)
            {
                identityContext.UserRoles.Remove(roleRep);
                await identityContext.SaveChangesAsync();
            }
        }
    }

}
