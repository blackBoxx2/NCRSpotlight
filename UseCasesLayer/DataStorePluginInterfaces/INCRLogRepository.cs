using EntitiesLayer.Models;

namespace Plugins.DataStore.SQLite
{
    public interface INCRLogSQLRepository
    {
        Task AddNCRLogAsync(NCRLog log);
        Task DeleteNCRLogAsync(int? id);
        Task<IEnumerable<NCRLog>> GetNCRLogAsync();
        Task<NCRLog> GetNCRLogByIDAsync(int? id);
        Task UpdateNCRLogAsync(int? id, NCRLog log);
    }
}