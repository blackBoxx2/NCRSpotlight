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
    public class GetNCRLogByIDAsyncUseCase : IGetNCRLogByIDAsyncUseCase
    {
        private readonly INCRLogRepository _repository;
        public GetNCRLogByIDAsyncUseCase(INCRLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<NCRLog> Execute(int? id)
        {
            return await _repository.GetNCRLogByIDAsync(id);
        }
    }
}
