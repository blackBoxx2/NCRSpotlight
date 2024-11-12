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
    public class EngineerPortionSQLRepository : IEngPortionRepository
    {
        private readonly NCRContext _context;

        public EngineerPortionSQLRepository(NCRContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EngPortion>> GetEngPortionsAsync()
        {
            return await _context.EngPortions
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<EngPortion> GetEngPortionByIDAsync(int? id)
        {
            if (id == null) return null;

            var eng = await _context.EngPortions
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID == id);

            return eng;

        }

        public async Task AddEngPortionAsync(EngPortion eng)
        {
            await _context.EngPortions.AddAsync(eng);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEngPortionAsync(int? id, EngPortion engPortion)
        {

            if (id == null) return;

            var engToUpdate = await _context.EngPortions.FirstOrDefaultAsync(x => x.ID == id);

            if (engToUpdate == null) return;

            engToUpdate.EngReview = engPortion.EngReview;
            engToUpdate.Notif = engPortion.Notif;
            engToUpdate.Disposition = engPortion.Disposition;
            engToUpdate.Update = engPortion.Update;
            engToUpdate.RevNumber = engPortion.RevNumber;
            engToUpdate.RevDate = engPortion.RevDate;
            engToUpdate.RepID = engPortion.RepID;
            await _context.SaveChangesAsync();


        }

        public async Task DeleteEngPortionAsync(int? id)
        {

            var eng = await _context.EngPortions.FirstOrDefaultAsync(x => x.ID == id);

            if (eng != null)
            {
                _context.EngPortions.Remove(eng);
                await _context.SaveChangesAsync();
            }



        }


    }
}
