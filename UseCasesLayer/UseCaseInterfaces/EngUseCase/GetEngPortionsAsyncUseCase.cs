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
    public class GetEngPortionsAsyncUseCase : IGetEngPortionsAsyncUseCase
    {
        private readonly IEngPortionRepository _engPortionRepository;
        public GetEngPortionsAsyncUseCase(IEngPortionRepository engPortionRepository)
        {
            _engPortionRepository = engPortionRepository;
        }       

        public async Task<IEnumerable<EngPortion>> Execute()
        {
            return await _engPortionRepository.GetEngPortionsAsync();
        }
    }
}
