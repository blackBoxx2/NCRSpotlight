using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Models;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.NCRLogUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.NCRLogUseCase
{
    public class UpdateNCRLogAsyncUseCase : IUpdateNCRLogAsyncUseCase
    {
        private readonly INCRLogRepository _repository;
        public UpdateNCRLogAsyncUseCase(INCRLogRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(int? id, NCRLog log)
        {
            await _repository.UpdateNCRLogAsync(id, log);
        }
    }
}
