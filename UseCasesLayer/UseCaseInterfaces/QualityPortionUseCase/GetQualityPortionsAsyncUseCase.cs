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
    public class GetQualityPortionsAsyncUseCase : IGetQualityPortionsAsyncUseCase
    {

        private readonly IQualityPortionSQLRepository _qualityPortionSQLRepository;
        public GetQualityPortionsAsyncUseCase(IQualityPortionSQLRepository qualityPortionSQLRepository)
        {

            _qualityPortionSQLRepository = qualityPortionSQLRepository;

        }

        public async Task<IEnumerable<QualityPortion>> Execute()
        {
            return await _qualityPortionSQLRepository.GetQualityPortionsAsync();
        }
    }
}
