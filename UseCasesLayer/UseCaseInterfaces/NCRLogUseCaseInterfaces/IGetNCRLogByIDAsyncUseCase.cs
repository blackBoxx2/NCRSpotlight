using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.UseCaseInterfaces.NCRLogUseCaseInterfaces
{
    public interface IGetNCRLogByIDAsyncUseCase
    {
        Task Execute(int? id);
    }
}
