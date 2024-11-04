using EntitiesLayer.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.UseCaseInterfaces.RepresentitiveUseCaseInterfaces
{
    public interface IDeleteRepresentativeAsyncUseCase
    {
        Task Execute(int id);
    }
}
