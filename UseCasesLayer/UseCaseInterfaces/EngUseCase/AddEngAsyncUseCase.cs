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
    public class AddEngAsyncUseCase : IAddEngAsyncUseCase
    {
        IEngPortionRepository _portionRepository;

        AddEngAsyncUseCase (IEngPortionRepository portionRepository)
        {
            _portionRepository = portionRepository;
        }

        public async Task Execute(EngPortion engPortion)
        {
            await _portionRepository.AddQualityPortionAsync(engPortion);
        }
    }
}
