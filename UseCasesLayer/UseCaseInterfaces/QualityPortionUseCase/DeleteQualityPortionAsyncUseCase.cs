using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.QualityPortionUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.QualityPortionUseCase
{
    public class DeleteQualityPortionAsyncUseCase : IDeleteQualityPortionAsyncUseCase
    {
        private readonly IQualityPortionSQLRepository _qualityPortionSQLRepository;
        public DeleteQualityPortionAsyncUseCase(IQualityPortionSQLRepository qualityPortionSQLRepository)
        {

            _qualityPortionSQLRepository = qualityPortionSQLRepository;

        }

        public async Task Execute(int id)
        {
            await _qualityPortionSQLRepository.DeleteQualityPortionAsync(id);
        }
    }
}
