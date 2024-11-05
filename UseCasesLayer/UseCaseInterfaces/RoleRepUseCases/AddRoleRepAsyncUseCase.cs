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
    public class AddRoleRepAsyncUseCase : IAddRoleRepAsyncUseCase
    {
        private readonly IRoleRepRepository _roleRepRepository;

        public AddRoleRepAsyncUseCase(IRoleRepRepository roleRepRepository)
        {
            this._roleRepRepository = roleRepRepository;
        }
        public async Task Execute(RoleRep roleRep)
        {
            await _roleRepRepository.AddRoleRepAsync(roleRep);
        }
    }
}
