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
    public class GetProductByIDAsyncUseCase : IGetProductByIDAsyncUseCase
    {

        private readonly IProductRepository _ProductRepository;

        public GetProductByIDAsyncUseCase(IProductRepository repository)
        {
            _ProductRepository = repository;
        }
        public async Task<Product> Execute(int? id)
        {
            return await _ProductRepository.GetProductByIDAsync(id);
        }
    }
}
