using EntitiesLayer.Models;
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
        private readonly NCRContext _context;

        public RoleRepSQLRepository(NCRContext context)
        {
            _context = context;
        }

        // list all roleRep
        public async Task<IEnumerable<RoleRep>> GetRoleRepASync()
        {
            var roleReps = await _context.RoleReps
                .Include(r => r.Representative)
                .Include(r => r.Role)
                .ToListAsync();
            return roleReps;
        }

       
        public async Task<RoleRep> GetRoleRepByIdAsync(int? id)
        {
            if (id == null) return null;

            var roleRep = await _context.RoleReps
                .Include(r => r.Representative)
                .Include(r => r.Role)
                .FirstOrDefaultAsync(m => m.RoleRepID == id);

            return roleRep;
        }

        // add a roleRep
        public async Task AddRoleRepAsync(RoleRep roleRep)
        {
            _context.RoleReps.Add(roleRep);
            await _context.SaveChangesAsync();
        }

        // edit roleRep
        public async Task UpdateRoleRepAsync(int? id, RoleRep roleRep)
        {
            if (id != roleRep.RoleRepID) return;

            var roleRepToUpdate = await _context.RoleReps
                                        .Include(r => r.Representative)
                                        .Include(r => r.Role)
                                        .FirstOrDefaultAsync(r => r.RoleRepID == id);
            if (roleRepToUpdate == null) return;

            roleRepToUpdate.RoleID = roleRep.RoleID;
            roleRepToUpdate.RepresentativeID = roleRep.RepresentativeID;
            await _context.SaveChangesAsync();
        }

        // delete roleRep
        public async Task DeleteRoleRepAsync(int id)
        {
            var roleRep = await _context.RoleReps
                                .Include(r => r.Representative)
                                .Include(r => r.Role)
                                .FirstOrDefaultAsync(r => r.RoleRepID == id);
            if (roleRep != null)
            {
                _context.RoleReps.Remove(roleRep);
                await _context.SaveChangesAsync();
            }
        }
    }

}
