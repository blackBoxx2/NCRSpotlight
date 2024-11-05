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
    public class GetNCRLogsAsyncUseCase : IGetNCRLogsAsyncUseCase
    {
        private readonly INCRLogRepository _repository;
        public GetNCRLogsAsyncUseCase(INCRLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<NCRLog>> GetNCRLogsAsync()
        {
            return await _repository.GetNCRLogAsync();
        }
    }
}
