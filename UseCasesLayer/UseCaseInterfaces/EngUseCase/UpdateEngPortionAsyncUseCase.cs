using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Models;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.EngUseCaseInterface;

namespace UseCasesLayer.UseCaseInterfaces.EngUseCase
{
    public class UpdateEngPortionAsyncUseCase : IUpdateEngPortionAsyncUseCase
    {

        private readonly IEngPortionRepository _engPortionRepository;
        public UpdateEngPortionAsyncUseCase(IEngPortionRepository engPortionRepository)
        {
            _engPortionRepository = engPortionRepository;
        }
        public async Task Execute(int? id, EngPortion engPortion)
        {
            await _engPortionRepository.UpdateEngPortionAsync(id, engPortion);
        }
    }
}
