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
    public class GetRoleRepAsyncUseCase : IGetRoleRepAsyncUseCase
    {
        private readonly IRoleRepRepository _roleRepRepository;

        public GetRoleRepAsyncUseCase(IRoleRepRepository roleRepRepository)
        {
            this._roleRepRepository = roleRepRepository;
        }
        public async Task<IEnumerable<RoleRep>> Execute()
        {
            return await _roleRepRepository.GetRoleRepASync();
        }

    }
}
