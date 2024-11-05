using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Models;

namespace UseCasesLayer.UseCaseInterfaces.NCRLogUseCaseInterfaces
{
    public interface IGetNCRLogByIDAsyncUseCase
    {
        Task<NCRLog> Execute(int? id);
    }
}
