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
    public class QualityPortionSQLRepository : IQualityPortionSQLRepository
    {

        private readonly NCRContext _context;

        public QualityPortionSQLRepository(NCRContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QualityPortion>> GetQualityPortionsAsync()
        {
            var NCRContext = await _context.QualityPortions
                .Include(q => q.Product)
                .Include(q => q.RoleRep)
                .Include(q => q.qualityPictures)
                .AsNoTracking()
                .ToListAsync();

            return NCRContext;
        }

        public async Task<QualityPortion> GetQualityPortionByIDAsync(int? id)
        {

            if (id == null) return null;

            var qualityPortion = await _context.QualityPortions
                .Include(q => q.Product)
                .Include(q => q.RoleRep)
                .Include(q => q.qualityPictures).ThenInclude(q => q.FileContent)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            return qualityPortion;


        }

        public async Task AddQualityPortionAsync(QualityPortion qualityPortion)
        {

            _context.QualityPortions.Add(qualityPortion);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateQualityPortionAsync(int? id, QualityPortion qualityPortion)
        {

            if (id != qualityPortion.ID) return;

            var qualityPortionToUpdate = await _context.QualityPortions
                .Include(q => q.Product)
                .Include(q => q.RoleRep)
                .Include(q => q.qualityPictures).ThenInclude(q => q.FileContent)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (qualityPortionToUpdate == null) return;

            qualityPortionToUpdate.ProductID = qualityPortion.ID;
            qualityPortionToUpdate.Quantity = qualityPortion.Quantity;
            qualityPortionToUpdate.QuantityDefective = qualityPortion.QuantityDefective;
            qualityPortionToUpdate.OrderNumber = qualityPortion.OrderNumber;
            qualityPortionToUpdate.DefectDescription = qualityPortion.DefectDescription;
            qualityPortionToUpdate.RoleRepID = qualityPortion.RoleRepID;
            qualityPortionToUpdate.qualityPictures = qualityPortion.qualityPictures;
            await _context.SaveChangesAsync();

        }

        public async Task DeleteQualityPortionAsync(int? id)
        {
            var qualityPortion = await _context.QualityPortions
                .Include(q => q.Product)
                .Include(q => q.RoleRep)
                .Include(q => q.qualityPictures).ThenInclude(q => q.FileContent)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (qualityPortion != null)
            {
                _context.QualityPortions.Remove(qualityPortion);
                await _context.SaveChangesAsync();
            }
        }


    }
}
