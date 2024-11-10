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
    public class DeleteRepresentativeAsyncUseCase : IDeleteRepresentativeAsyncUseCase
    {
        private readonly IRepresentativeRepository _representativeRepository;

        public DeleteRepresentativeAsyncUseCase(IRepresentativeRepository representativeRepository)
        {
            this._representativeRepository = representativeRepository;
        }
        public async Task Execute(string id)
        {
            await _representativeRepository.DeleteRepresentativeAsync(id);
        }

        public Task Execute(Representative representative)
        {
            throw new NotImplementedException();
        }
    }
}
