using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugins.DataStore.SQLite;
using UseCasesLayer.UseCaseInterfaces.EngUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.EngUseCase
{
    public class DeleteEngAsyncUseCase : IDeleteEngAsyncUseCase
    {

        IEngPortionRepository _portionRepository;

        DeleteEngAsyncUseCase(IEngPortionRepository portionRepository)
        {
            _portionRepository = portionRepository;
        }

        public async Task Execute(int? id)
        {
           await _portionRepository.DeleteEngPortionAsync(id);
        }
    }
}
