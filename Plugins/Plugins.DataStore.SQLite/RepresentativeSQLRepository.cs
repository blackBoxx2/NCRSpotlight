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
        public async Task<Representative> GetRepresentativesByIdAsync(int? id)
        {
            if (id == null) return null;

            var representative = await _context.Representatives
                .Include(r => r.RoleReps)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            return representative;
        }

        // Add a representitive
        public async Task AddRepresentativeAsync(Representative representative)
        {
            _context.Representatives.Add(representative);
            await _context.SaveChangesAsync();
        }

        //Edit a rep
        public async Task UpdateRepresentativeAsync(int? id, Representative representative)
        {
            if (id != representative.ID) return;

            var repToUpdate = await _context.Representatives
                .Include(r => r.RoleReps)
                .FirstOrDefaultAsync(r => r.ID == id);
            if (representative == null) return;

            repToUpdate.FirstName = representative.FirstName;
            if(representative.MiddleInitial != null)
                repToUpdate.MiddleInitial = representative.MiddleInitial;
            repToUpdate.LastName = representative.LastName;
            await _context.SaveChangesAsync();

        }

        // Delete a rep

        public async Task DeleteRepresentativeAsync(int id)
        {
            var representative = await _context.Representatives
                .Include(r => r.RoleReps)
                .FirstOrDefaultAsync(r => r.ID == id);

            if (representative != null)
            {
                if (representative.RoleReps.Count() > 0)
                {
                    throw new Exception("Error: You cannot delete this representative because it has attached roles.");
                }

                _context.Representatives.Remove(representative);
                await _context.SaveChangesAsync();
            }
        }


    }
}
