using EntitiesLayer.Models;

namespace Plugins.DataStore.SQLite
{
    public interface IQualityPortionSQLRepository
    {
        Task AddQualityPortionAsync(QualityPortion qualityPortion);
        Task DeleteQualityPortionAsync(int? id);
        Task<QualityPortion> GetQualityPortionByIDAsync(int? id);
        Task<IEnumerable<QualityPortion>> GetQualityPortionsAsync();
        Task UpdateQualityPortionAsync(int? id, QualityPortion qualityPortion);
    }
}