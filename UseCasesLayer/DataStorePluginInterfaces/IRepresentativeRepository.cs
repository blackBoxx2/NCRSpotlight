using EntitiesLayer.Models;

namespace Plugins.DataStore.SQLite
{
    public interface IRepresentativeRepository
    {
        Task AddRepresentativeAsync(Representative representative);
        Task DeleteRepresentativeAsync(int id);
        Task<IEnumerable<Representative>> GetRepresentativesAsync();
        Task<Representative> GetRepresentativesByIdAsync(int? id);
        Task UpdateRepresentativeAsync(int? id, Representative representative);
    }
}