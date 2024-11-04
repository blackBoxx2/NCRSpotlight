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
    public class SupplierSQLRepository : ISupplierRepository
    { 
        private readonly NCRContext _context;

        public SupplierSQLRepository(NCRContext context)
        {
            _context = context;
        }

        //List All Suppliers
        public async Task<IEnumerable<Supplier>> GetSuppliersAsyc()
        {
            var suppliers = await _context.Suppliers
                .Include(s => s.Products)
                .AsNoTracking()
                .ToListAsync();

            return suppliers;
        }

        public async Task<Supplier> GetSupplierByIdAsync(int? id)
        {
            if (id == null) return null;
            var supplier = await _context.Suppliers
                    .Include(s => s.Products)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.ID == id);
           return supplier;
        }

        public async Task AddSupplierAsyc(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }

        //Edit Supplier -- gonna move this to controller to use TryUpdateModelAsyc
        public async Task UpdateSupplierAsyc(int? id, Supplier supplier)
        {
            if (id != supplier.ID) return;

            var supplierToUpdate = await _context.Suppliers
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (supplier == null) return;

            supplierToUpdate.SupplierName = supplier.SupplierName;
            await _context.SaveChangesAsync();
        }

        //Delete Supplier
        public async Task DeleteSupplierAsyc(int id)
        {
            var supplier = await _context.Suppliers
                .Include(s => s.Products)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (supplier != null)
            {
                if (supplier.Products.Count() > 0)
                {
                    throw new InvalidOperationException("You cannot delete a supplier that has products in the system");
                }
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
        }

    }
}
