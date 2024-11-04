using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Models;

namespace UseCasesLayer.DataStorePluginInterfaces
{
    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIDAsync(int? id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(int? id, Product product);
        Task DeleteProductAsync(int? id);
        


    }
}
