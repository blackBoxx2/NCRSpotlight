using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.RepresentitiveUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.RoleRepUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.RoleRepUseCases
{
    public class DeleteRoleRepAsyncUseCase : IDeleteRoleRepAsyncUseCase 
    {
        private readonly IRoleRepRepository _roleRepRepository;

        public DeleteRoleRepAsyncUseCase(IRoleRepRepository roleRepRepository)
        {
            this._roleRepRepository = roleRepRepository;
        }
        public async Task Execute(string id)
        {
            await _roleRepRepository.DeleteRoleRepAsync(id);
        }
    }
}
