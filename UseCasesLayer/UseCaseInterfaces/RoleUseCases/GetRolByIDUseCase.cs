using EntitiesLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.RoleUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.RoleUseCases
{
    public class GetRolByIDUseCase : IGetRoleByIDAsyncUserCase
    {
        private readonly IRoleRepository _roleRepository;

        public GetRolByIDUseCase(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public async Task<IdentityRole> Execute(string id)
        {
            return await _roleRepository.GetRoleByIDAsync(id);
        }
    }
}
