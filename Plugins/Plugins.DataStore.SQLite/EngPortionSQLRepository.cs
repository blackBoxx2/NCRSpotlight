using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Plugins.DataStore.SQLite
{
    public class EngPortionSQLRepository : IEngPortionRepository
    {

        private readonly NCRContext _context;

        public EngPortionSQLRepository(NCRContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EngPortion>> GetEngPortionsAsync()
        {
            var portions = await _context.EngPortions.AsNoTracking().ToListAsync();

            return portions;
        }

        public async Task<EngPortion> GetEngPortionByIDAsync(int? id)
        {
            if (id == null) return null;

            var portion = await _context.EngPortions
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ID == id);

            return portion;

        }

        public async Task AddQualityPortionAsync(EngPortion engPortion)
        {

            _context.EngPortions.Add(engPortion);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateQualityPortionAsync(int? id, EngPortion engPortion)
        {
            if (id == null) return;

            var engPortionToUpdate = await _context.EngPortions
                .FirstOrDefaultAsync(e => e.ID == id);

            if (engPortionToUpdate == null) return;


            engPortionToUpdate.EngReview = engPortion.EngReview;
            engPortionToUpdate.Notif = engPortion.Notif;
            engPortionToUpdate.Disposition = engPortion.Disposition;
            engPortionToUpdate.Update = engPortion.Update;
            engPortionToUpdate.RevNumber = engPortion.RevNumber;

        }

        public async Task DeleteEngPortionAsync(int? id)
        {
            if (id == null) return;

            var engPortion = await _context.EngPortions
                .FirstOrDefaultAsync(e => e.ID == id);

            if (engPortion != null)
            {
                _context.EngPortions.Remove(engPortion);
                _context.SaveChanges();
            }
        }
    }
}
