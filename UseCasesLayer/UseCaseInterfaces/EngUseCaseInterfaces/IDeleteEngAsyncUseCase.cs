using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.UseCaseInterfaces.EngUseCaseInterfaces
{
     public interface IDeleteEngAsyncUseCase
    {
        Task Execute(int? id);
    }
}
