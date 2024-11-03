using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.SuppliersUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.SuppliersUseCases
{
    public class DeleteSupplierAsyncUseCase : IDeleteSupplierAsyncUseCase
    {
        private readonly ISupplierRepository _supplierRepository;

        public DeleteSupplierAsyncUseCase(ISupplierRepository supplierRepository)
        {
            this._supplierRepository = supplierRepository;
        }
        public async Task Execute(int id)
        {
            await _supplierRepository.DeleteSupplierAsyc(id);
        }
    }
}
