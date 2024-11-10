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
    public class UpdateRoleAsyncUseCase : IUpdateRoleAsyncUserCase
    {
        private readonly IRoleRepository _roleRepository;

        public UpdateRoleAsyncUseCase(IRoleRepository roleRepository) 
        {
            this._roleRepository = roleRepository;
        }
        public async Task Execute(string id, IdentityRole role)
        {
           await _roleRepository.UpdateRoleAsync(id, role);
        }
    }
}
