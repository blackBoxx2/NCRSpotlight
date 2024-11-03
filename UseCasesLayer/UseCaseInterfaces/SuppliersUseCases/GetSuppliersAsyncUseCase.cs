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
    public class GetSuppliersAsyncUseCase : IGetSuppliersAsyncUseCase
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetSuppliersAsyncUseCase(ISupplierRepository supplierRepository)
        {
            this._supplierRepository = supplierRepository;
        }
        public async Task<IEnumerable<Supplier>> Execute()
        {
            return await _supplierRepository.GetSuppliersAsyc();
        }
    }
}
