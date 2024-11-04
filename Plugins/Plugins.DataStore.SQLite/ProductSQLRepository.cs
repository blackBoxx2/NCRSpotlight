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
    public class ProductSQLRepository : IProductRepository
    {

        private readonly NCRContext _context;

        public ProductSQLRepository(NCRContext context)
        {

            _context = context;

        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var NCRContext = await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.ProductPictures)
                .AsNoTracking()
                .ToListAsync();

            return NCRContext;
        }

        public async Task<Product> GetProductByIDAsync(int? id)
        {
            if (id == null) return null;

            var product = await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.ProductPictures).ThenInclude(p => p.FileContent)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            return product;
        }

        public async Task AddProductAsync(Product product)
        {

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateProductAsync(int? id, Product product)
        {
            if (id != product.ID) return;

            var productToUpdate = await _context.Products
                .Include(p => p.Supplier)
                .Include(p => p.ProductPictures).ThenInclude(p => p.FileContent)
                .FirstOrDefaultAsync(p => p.ID == id);
            if (productToUpdate == null) return;

            productToUpdate.SupplierID = product.SupplierID;
            productToUpdate.ProductNumber = product.ProductNumber;
            productToUpdate.Description = product.Description;
            productToUpdate.ProductPictures = product.ProductPictures;
            await _context.SaveChangesAsync();

        }

        public async Task DeleteProductAsync(int? id)
        {
            var product = await _context.Products
                .Include(p => p.QualityPortions)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (product != null)
            {
                if (product.QualityPortions.Count() > 0)
                {
                    throw new InvalidOperationException("You cannot delete a product that has NCR's in the system");
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

    }
}
