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
    public class UpdateRoleRepAsyncUseCase : IUpdateRoleRepAsyncUseCase
    {
        private readonly IRoleRepRepository _roleRepRepository;

        public UpdateRoleRepAsyncUseCase(IRoleRepRepository roleRepRepository)
        {
            this._roleRepRepository = roleRepRepository;
        }
        public async Task Execute(string id, IdentityUserRole<string> roleRep)
        {
            await _roleRepRepository.UpdateRoleRepAsync(id, roleRep);
        }
    }
}
