using EntitiesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.RoleRepUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.RoleRepUseCases
{
    public class GetRoleRepByIdAsyncUseCase : IGetRoleRepByIDAsyncUseCase
    {
        private readonly IRoleRepRepository _roleRepRepository;

        public GetRoleRepByIdAsyncUseCase(IRoleRepRepository roleRepRepository)
        {
            this._roleRepRepository = roleRepRepository;
        }

        public async Task<RoleRep> Execute(int? id)
        {
            return await _roleRepRepository.GetRoleRepByIdAsync(id);

        }
    }
}
