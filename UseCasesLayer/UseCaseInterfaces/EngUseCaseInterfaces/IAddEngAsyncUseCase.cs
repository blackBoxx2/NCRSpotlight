﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Models;

namespace UseCasesLayer.UseCaseInterfaces.EngUseCaseInterfaces
{
    public interface IAddEngAsyncUseCase
    {
        Task Execute(EngPortion engPortion);

    }
}
