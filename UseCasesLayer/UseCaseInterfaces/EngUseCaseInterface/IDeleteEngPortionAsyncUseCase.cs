using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.UseCaseInterfaces.EngUseCaseInterface
{
     public interface IDeleteEngPortionAsyncUseCase
    {

        Task Execute(int? id);

    }
}
