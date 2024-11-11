using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Models;
using Plugins.DataStore.SQLite;
using UseCasesLayer.UseCaseInterfaces.EngUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.EngUseCase
{
    public class GetEngAsyncUseCase : IGetEngAsyncUseCase
    {
        IEngPortionRepository _portionRepository;

        GetEngAsyncUseCase(IEngPortionRepository portionRepository)
        {
            _portionRepository = portionRepository;
        }
        public async Task<IEnumerable<EngPortion>> Execute()
        {
            return await _portionRepository.GetEngPortionsAsync();
        }
    }
}
