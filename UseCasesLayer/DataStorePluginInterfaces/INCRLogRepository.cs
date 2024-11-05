using EntitiesLayer.Models;

namespace UseCasesLayer.DataStorePluginInterfaces
{
    public interface INCRLogRepository
    {
        Task AddNCRLogAsync(NCRLog log);
        Task DeleteNCRLogAsync(int? id);
        Task<IEnumerable<NCRLog>> GetNCRLogAsync();
        Task<NCRLog> GetNCRLogByIDAsync(int? id);
        Task UpdateNCRLogAsync(int? id, NCRLog log);
    }
}