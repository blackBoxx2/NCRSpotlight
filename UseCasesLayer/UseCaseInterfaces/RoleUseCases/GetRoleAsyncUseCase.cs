using EntitiesLayer.Models;
using Plugins.DataStore.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Role>> Execute()
        {
            return await _roleRepository.GetRolesAsync();
        }
    }
}
