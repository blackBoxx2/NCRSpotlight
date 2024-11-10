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
    public class GetRoleAsyncUseCase : IGetRoleAsyncUserCase
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleAsyncUseCase(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public async Task<IEnumerable<IdentityRole>> Execute()
        {
            return await _roleRepository.GetRolesAsync();
        }
    }
}
