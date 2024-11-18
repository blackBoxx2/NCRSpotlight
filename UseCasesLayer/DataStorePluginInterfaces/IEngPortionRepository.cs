using EntitiesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.DataStorePluginInterfaces
{
    public interface IEngPortionRepository
    {

        Task AddEngPortionAsync(EngPortion eng);
        Task DeleteEngPortionAsync(int? id);
        Task<EngPortion> GetEngPortionByIDAsync(int? id);
        Task<IEnumerable<EngPortion>> GetEngPortionsAsync();
        Task UpdateEngPortionAsync(int? id, EngPortion engPortion);

    }
}
