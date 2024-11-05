using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.NCRLogUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.NCRLogUseCase
{
    public class DeleteNCRLogAsyncUseCase : IDeleteNCRLogAsyncUseCase
    {

        private readonly INCRLogRepository _repository;
        public DeleteNCRLogAsyncUseCase(INCRLogRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(int? id)
        {
            await _repository.DeleteNCRLogAsync(id);
        }
    }
}
