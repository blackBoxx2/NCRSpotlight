using EntitiesLayer.Models;

namespace Plugins.DataStore.SQLite
{
    public interface IEngPortionRepository
    {
        Task AddQualityPortionAsync(EngPortion engPortion);
        Task DeleteEngPortionAsync(int? id);
        Task<EngPortion> GetEngPortionByIDAsync(int? id);
        Task<IEnumerable<EngPortion>> GetEngPortionsAsync();
        Task UpdateQualityPortionAsync(int? id, EngPortion engPortion);
    }
}