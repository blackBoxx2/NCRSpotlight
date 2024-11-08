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
    public class GetQualityPortionByIDAsyncUseCase : IGetQualityPortionByIDAsyncUseCase
    {
        private readonly IQualityPortionSQLRepository _qualityPortionSQLRepository;
        public GetQualityPortionByIDAsyncUseCase(IQualityPortionSQLRepository qualityPortionSQLRepository)
        {

            _qualityPortionSQLRepository = qualityPortionSQLRepository;

        }

        public async Task<QualityPortion> Execute(int? id)
        {
            return await _qualityPortionSQLRepository.GetQualityPortionByIDAsync(id);
        }
    }
}
