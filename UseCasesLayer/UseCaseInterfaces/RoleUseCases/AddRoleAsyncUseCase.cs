﻿using EntitiesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCasesLayer.DataStorePluginInterfaces;
using UseCasesLayer.UseCaseInterfaces.RoleUseCaseInterfaces;

namespace UseCasesLayer.UseCaseInterfaces.RoleUseCases
{
    public class AddRoleAsyncUseCase : IAddRoleAsyncUserCase
    {
        private readonly IRoleRepository _roleRepository;

        public AddRoleAsyncUseCase(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public async Task Execute(Role Role)
        {
            await _roleRepository.AddRoleAsync(Role);
        }
    }
}
