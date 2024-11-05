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
    public class AddProductAsyncUseCase : IAddProductAsyncUseCase
    {

        private readonly IProductRepository _ProductRepository;

        public AddProductAsyncUseCase(IProductRepository repository)
        {
            _ProductRepository = repository;
        }

        public async Task Execute(Product product)
        {
            await _ProductRepository.AddProductAsync(product);
        }
    }
}
