using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Models;
using Microsoft.EntityFrameworkCore;
using UseCasesLayer.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQLite
{
    public class NCRLogSQLRepository : INCRLogRepository
    {

        private readonly NCRContext _context;

        public NCRLogSQLRepository(NCRContext context)
        {

            _context = context;

        }

        public async Task<IEnumerable<NCRLog>> GetNCRLogAsync()
        {
            var NCRContext = await _context.NCRLog
                .Include(p => p.QualityPortion)
                .AsNoTracking()
                .ToListAsync();

            return NCRContext;
        }

        public async Task<NCRLog> GetNCRLogByIDAsync(int? id)
        {
            if (id == null) return null;

            var log = await _context.NCRLog
                .Include(p => p.QualityPortion)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            return log;
        }

        public async Task AddNCRLogAsync(NCRLog log)
        {

            _context.NCRLog.Add(log);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateNCRLogAsync(int? id, NCRLog log)
        {
            if (id != log.ID) return;

            var logToUpdate = await _context.NCRLog
                .Include(p => p.QualityPortion)
                .FirstOrDefaultAsync(p => p.ID == id);
            if (logToUpdate == null) return;

            logToUpdate.QualityPortionID = log.QualityPortionID;
            logToUpdate.Status = log.Status;

            await _context.SaveChangesAsync();

        }

        public async Task DeleteNCRLogAsync(int? id)
        {
            var log = await _context.NCRLog
                .Include(p => p.QualityPortion)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (log != null)
            {
                _context.NCRLog.Remove(log);
                await _context.SaveChangesAsync();
            }
        }
    }
}
