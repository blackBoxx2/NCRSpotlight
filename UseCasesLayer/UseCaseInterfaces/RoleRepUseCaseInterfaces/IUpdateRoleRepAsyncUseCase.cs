﻿using EntitiesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.UseCaseInterfaces.RoleRepUseCaseInterfaces
{
    public interface IUpdateRoleRepAsyncUseCase
    {
        Task Execute(int? id, RoleRep roleRep);
    }
}
