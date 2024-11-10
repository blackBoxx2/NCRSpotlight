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
    public class RepresentativeSQLRepository : IRepresentativeRepository
    {
        private readonly NCRContext _context;
        private readonly IdentityContext identityContext;

        public RepresentativeSQLRepository(NCRContext context, IdentityContext identityContext)
        {
            _context = context;
            this.identityContext = identityContext;
        }

        // List: Representatives
        public async Task<IEnumerable<IdentityUser>> GetRepresentativesAsync()
        {
            //var representitives = await _context.Representatives
            //    .Include(r => r.RoleReps)
            //    .AsNoTracking()
            //    .ToListAsync();

            var representitives = await identityContext.Users
                .AsNoTracking()
                .ToListAsync();

            return representitives;
        }
        public async Task<IdentityUser> GetRepresentativesByIdAsync(string id)
        {
            if (id == null) return null;

            var representative = await identityContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            return representative;
        }

        // Add a representitive
        public async Task AddRepresentativeAsync(IdentityUser representative)
        {
            identityContext.Users.Add(representative);
            await identityContext.SaveChangesAsync();
        }

        //Edit a rep
        public async Task UpdateRepresentativeAsync(string id, IdentityUser representative)
        {
            if (id != representative.Id) return;

            var repToUpdate = await identityContext.Users
                .FirstOrDefaultAsync(r => r.Id == id);
            if (representative == null) return;

            repToUpdate.UserName = representative.UserName;
            repToUpdate.Email = representative.Email;
            await identityContext.SaveChangesAsync();

        }

        // Delete a rep

        public async Task DeleteRepresentativeAsync(string id)
        {
            var representative = await identityContext.Users
                .FirstOrDefaultAsync(r => r.Id == id);

            if (representative != null)
            {
                //if (representative.RoleReps.Count() > 0)
                //{
                //    throw new Exception("Error: You cannot delete this representative because it has attached roles.");
                //}

                identityContext.Users.Remove(representative);
                await identityContext.SaveChangesAsync();
            }
        }


    }
}
