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
    public class GetEngPortionByIDAsyncUseCase : IGetEngPortionsByIDAsyncUseCase
    {

        private readonly IEngPortionRepository _engPortionRepository;
        public GetEngPortionByIDAsyncUseCase(IEngPortionRepository engPortionRepository)
        {
            _engPortionRepository = engPortionRepository;
        }
        public async Task<EngPortion> Execute(int? id)
        {
            return await _engPortionRepository.GetEngPortionByIDAsync(id);
        }
    }
}
