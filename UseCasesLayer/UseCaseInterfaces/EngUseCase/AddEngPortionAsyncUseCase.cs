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
    public class AddEngPortionAsyncUseCase : IAddEngPortionAsyncUseCase
    {

        private readonly IEngPortionRepository _engineerPortionRepository;


        public AddEngPortionAsyncUseCase(IEngPortionRepository engineerPortionRepository)
        {

            _engineerPortionRepository = engineerPortionRepository;

        }

        public async Task Execute(EngPortion eng)
        {
            await  _engineerPortionRepository.AddEngPortionAsync(eng);
        }
    }
}
