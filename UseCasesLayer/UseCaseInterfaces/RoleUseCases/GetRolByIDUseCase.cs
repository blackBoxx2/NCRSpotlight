using EntitiesLayer.Models;
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

        public async Task<Role> Execute(int? id)
        {
            return await _roleRepository.GetRoleByIDAsync(id);
        }
    }
}
