using EntitiesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.RepresentitiveUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.RepresentativesUseCase
{
    public class AddRepresentativeAsyncUseCase : IAddRepresentativeAsyncUseCase
    {
        private readonly IRepresentativeRepository _representativeRepository;

        public AddRepresentativeAsyncUseCase(IRepresentativeRepository representativeRepository)
        {
            this._representativeRepository = representativeRepository;
        }
        public async Task Execute(Representative representative)
        {
            await _representativeRepository.AddRepresentativeAsync(representative);
        }
    }
}
