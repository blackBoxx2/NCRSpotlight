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
    public class AddSupplierAsyncUseCase : IAddSupplierAsyncUseCase
    {
        private readonly ISupplierRepository _supplierRepository;

        public AddSupplierAsyncUseCase(ISupplierRepository supplierRepository)
        {
            this._supplierRepository = supplierRepository;
        }
        public async Task Execute(Supplier supplier)
        {
            await _supplierRepository.AddSupplierAsyc(supplier);
        }
    }
}
