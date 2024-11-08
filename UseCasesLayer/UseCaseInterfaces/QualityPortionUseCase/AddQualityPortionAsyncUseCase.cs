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
    public class AddQualityPortionAsyncUseCase : IAddQualityPortionAsyncUseCase
    {
        private readonly IQualityPortionSQLRepository _qualityPortionSQLRepository;
        public AddQualityPortionAsyncUseCase(IQualityPortionSQLRepository qualityPortionSQLRepository)
        {

            _qualityPortionSQLRepository = qualityPortionSQLRepository;

        }
        public async Task Execute(QualityPortion qualityPortion)
        {
            await _qualityPortionSQLRepository.AddQualityPortionAsync(qualityPortion);
        }
    }
}
