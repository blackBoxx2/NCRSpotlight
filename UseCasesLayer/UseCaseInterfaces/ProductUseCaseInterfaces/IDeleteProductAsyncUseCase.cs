using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.UseCaseInterfaces.ProductUseCaseInterfaces
{
    public interface IDeleteProductAsyncUseCase
    {

        Task Execute(int? id);

    }
}
