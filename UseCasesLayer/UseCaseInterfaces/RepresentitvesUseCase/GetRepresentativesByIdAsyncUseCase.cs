using EntitiesLayer.Models;
using Plugins.DataStore.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.RepresentitiveUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.RepresentitvesUseCase
{
    public class GetRepresentativesByIdAsyncUseCase : IGetRepresentativesByIdAsyncUseCase
    {
        private readonly IRepresentativeRepository _representativeRepository;

        public GetRepresentativesByIdAsyncUseCase(IRepresentativeRepository representativeRepository)
        {
            this._representativeRepository = representativeRepository;
        }

        public async Task<Representative> Execute(int? id)
        {
            return await _representativeRepository.GetRepresentativesByIdAsync(id);

        }
    }
}
