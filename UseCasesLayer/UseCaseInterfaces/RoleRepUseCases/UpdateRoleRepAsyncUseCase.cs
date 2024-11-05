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
    public class UpdateRoleRepAsyncUseCase : IUpdateRoleRepAsyncUseCase
    {
        private readonly IRoleRepRepository _roleRepRepository;

        public UpdateRoleRepAsyncUseCase(IRoleRepRepository roleRepRepository)
        {
            this._roleRepRepository = roleRepRepository;
        }
        public async Task Execute(int? id, RoleRep roleRep)
        {
            await _roleRepRepository.UpdateRoleRepAsync(id, roleRep);
        }
    }
}
