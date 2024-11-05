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
        private readonly INCRLogRepository _logRepository;

        public GetNCRLogsAsyncUseCase(INCRLogRepository nCRLogRepository)
        {
            _logRepository = nCRLogRepository;
        }

        public async Task<IEnumerable<NCRLog>> Execute()
        {
            return await _logRepository.GetNCRLogAsync();
        }
    }
}
