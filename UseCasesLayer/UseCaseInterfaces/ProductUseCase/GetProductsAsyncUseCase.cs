using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Models;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.ProductUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.ProductUseCases
{
    public class GetProductsAsyncUseCase : IGetProductsAsyncUseCase
    {

        private readonly IProductRepository _productRepository;

        public GetProductsAsyncUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Execute()
        {
           return await _productRepository.GetProductsAsync();
        }
    }
}
