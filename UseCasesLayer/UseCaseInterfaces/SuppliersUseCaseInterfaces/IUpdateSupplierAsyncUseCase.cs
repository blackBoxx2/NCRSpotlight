﻿using EntitiesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.UseCaseInterfaces.SuppliersUseCaseInterfaces
{
    public interface IUpdateSupplierAsyncUseCase
    {
        Task Execute(int? id, Supplier supplier);

    }
}
