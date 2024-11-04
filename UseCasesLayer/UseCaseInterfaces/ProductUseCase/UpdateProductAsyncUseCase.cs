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
    public class UpdateProductAsyncUseCase : IUpdateProductAsyncUseCase
    {

        private readonly IProductRepository _ProductRepository;

        public UpdateProductAsyncUseCase(IProductRepository repository)
        {
            _ProductRepository = repository;
        }

        public async Task Execute(int? id, Product product)
        {
            await _ProductRepository.UpdateProductAsync(id, product);
        }
    }
}
