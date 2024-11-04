using Plugins.DataStore.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.RoleUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.RoleUseCases
{

    public class DeleteRoleAsyncUseCase : IDeleteRoleAsyncUserCase
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleAsyncUseCase(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }
        public async Task Execute(int id)
        {
           await _roleRepository.DeleteRoleAsync(id);
        }
    }
}
