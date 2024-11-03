using EntitiesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.SuppliersUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.SuppliersUseCases
{
    public class GetSupplierByIdAsyncUseCase : IGetSupplierByIDAsyncUseCase
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetSupplierByIdAsyncUseCase(ISupplierRepository supplierRepository)
        {
            this._supplierRepository = supplierRepository;
        }

        public async Task<Supplier> Execute(int? id)
        {
            return await _supplierRepository.GetSupplierByIdAsync(id);
            
        }
    }
}
