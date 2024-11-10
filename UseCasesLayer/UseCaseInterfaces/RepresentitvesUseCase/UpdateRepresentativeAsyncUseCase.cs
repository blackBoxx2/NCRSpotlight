using EntitiesLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.RepresentitiveUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.RepresentitvesUseCase
{
    public class UpdateRepresentativeAsyncUseCase : IUpdateRepresentativeAsyncUseCase
    {
        private readonly IRepresentativeRepository _representativeRepository;

        public UpdateRepresentativeAsyncUseCase(IRepresentativeRepository representativeRepository)
        {
            this._representativeRepository = representativeRepository;
        }
        public async Task Execute(string id, IdentityUser representative)
        {
            await _representativeRepository.UpdateRepresentativeAsync(id, representative);
        }
    }
}
