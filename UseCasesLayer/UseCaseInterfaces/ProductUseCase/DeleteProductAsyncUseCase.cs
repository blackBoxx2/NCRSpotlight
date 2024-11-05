using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.ProductUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.ProductUseCases
{
    public class DeleteProductAsyncUseCase : IDeleteProductAsyncUseCase
    {

        private readonly IProductRepository _ProductRepository;

        public DeleteProductAsyncUseCase(IProductRepository repository)
        {
            _ProductRepository = repository;
        }

        public async Task Execute(int? id)
        {
            await _ProductRepository.DeleteProductAsync(id);
        }
    }
}
