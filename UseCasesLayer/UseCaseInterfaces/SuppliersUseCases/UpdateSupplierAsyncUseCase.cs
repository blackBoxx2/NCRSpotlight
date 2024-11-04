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
    public class UpdateSupplierAsyncUseCase : IUpdateSupplierAsyncUseCase
    {
        private readonly ISupplierRepository _supplierRepository;

        public UpdateSupplierAsyncUseCase(ISupplierRepository supplierRepository)
        {
            this._supplierRepository = supplierRepository;
        }
        public async Task Execute(int? id, Supplier supplier)
        {
            await _supplierRepository.UpdateSupplierAsync(id,supplier); 
        }
    }
}
