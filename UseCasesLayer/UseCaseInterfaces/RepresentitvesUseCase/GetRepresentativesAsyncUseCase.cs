﻿using EntitiesLayer.Models;
using Plugins.DataStore.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.RepresentitiveUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.RepresentativesUseCase
{
    public class GetRepresentativesAsyncUseCase : IGetRepresentativesAsyncUseCase
    {
        private readonly IRepresentativeRepository _representativeRepository;

        public GetRepresentativesAsyncUseCase(IRepresentativeRepository representativeRepository)
        {
            this._representativeRepository = representativeRepository;
        }
        public async Task<IEnumerable<Representative>> Execute()
        {
            return await _representativeRepository.GetRepresentativesAsync();
        }
    }
}