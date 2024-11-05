using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.UseCaseInterfaces.NCRLogUseCaseInterfaces
{
    public interface IDeleteNCRLogAsyncUseCase
    {
        Task Execute(int? id);
    }
}
