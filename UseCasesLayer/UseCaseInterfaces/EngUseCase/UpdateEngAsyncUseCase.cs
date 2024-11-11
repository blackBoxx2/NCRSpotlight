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
    public class UpdateEngAsyncUseCase : IUpdateEngAsyncUseCase
    {

        IEngPortionRepository _portionRepository;

        UpdateEngAsyncUseCase(IEngPortionRepository portionRepository)
        {
            _portionRepository = portionRepository;
        }

        public async Task Execute(int? id, EngPortion engPortion)
        {
            await _portionRepository.UpdateQualityPortionAsync(id, engPortion);
        }
    }
}
