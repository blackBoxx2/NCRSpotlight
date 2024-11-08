using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Models;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.QualityPortionUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.QualityPortionUseCase
{
    public class UpdateQualityPortionAsyncUseCase : IUpdateQualityPortionAsyncUseCase
    {

        private readonly IQualityPortionSQLRepository _qualityPortionSQLRepository;
        public UpdateQualityPortionAsyncUseCase(IQualityPortionSQLRepository qualityPortionSQLRepository)
        {
            _qualityPortionSQLRepository = qualityPortionSQLRepository;
        }

        public async Task Execute(int? id, QualityPortion qualityPortion)
        {
            await _qualityPortionSQLRepository.UpdateQualityPortionAsync(id, qualityPortion);
        }
    }
}
