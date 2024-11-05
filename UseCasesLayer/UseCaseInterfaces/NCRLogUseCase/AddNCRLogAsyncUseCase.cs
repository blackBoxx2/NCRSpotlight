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
    public class AddNCRLogAsyncUseCase : IAddNCRLogAsyncUseCase
    {
        private readonly INCRLogRepository _repository;
        public AddNCRLogAsyncUseCase(INCRLogRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(NCRLog ncrLog)
        {
            await _repository.AddNCRLogAsync(ncrLog);
        }
    }
}
