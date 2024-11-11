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
    public class GetEngByIDAsyncUseCase : IGetEngByIDAsyncUseCase
    {
        IEngPortionRepository _portionRepository;

        GetEngByIDAsyncUseCase(IEngPortionRepository portionRepository)
        {
            _portionRepository = portionRepository;
        }
        public async Task<EngPortion> Execute(int? id)
        {
           return await _portionRepository.GetEngPortionByIDAsync(id);
        }
    }
}
