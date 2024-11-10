using EntitiesLayer.Models;
using Microsoft.AspNetCore.Identity;
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

        public async Task<IdentityUserRole<string>> Execute(string id)
        {
            return await _roleRepRepository.GetRoleRepByIdAsync(id);

        }
    }
}
