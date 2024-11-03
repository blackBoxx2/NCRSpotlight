using EntitiesLayer.Models;

namespace UseCasesLayer.DataStorePluginInterfaces
{
    public interface ISupplierRepository
    {
        Task<Supplier> GetSupplierByIdAsync(int? id);
        Task AddSupplierAsyc(Supplier supplier);
        Task DeleteSupplierAsyc(int id);
        Task<IEnumerable<Supplier>> GetSuppliersAsyc();
        Task UpdateSupplierAsync(int? id, Supplier supplier);
    }
}