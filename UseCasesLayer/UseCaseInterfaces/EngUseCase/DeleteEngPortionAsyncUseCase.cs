using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.EngUseCaseInterface;

namespace UseCasesLayer.UseCaseInterfaces.EngUseCase
{
    public class DeleteEngPortionAsyncUseCase : IDeleteEngPortionAsyncUseCase
    {

        private readonly IEngPortionRepository _engPortionRepository;

        public DeleteEngPortionAsyncUseCase(IEngPortionRepository engPortionRepository)
        {
            
            _engPortionRepository = engPortionRepository;

        }

        public async Task Execute(int? id)
        {
            await _engPortionRepository.DeleteEngPortionAsync(id);
        }
    }
}
