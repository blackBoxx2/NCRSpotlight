using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Models;

namespace UseCasesLayer.UseCaseInterfaces.ProductUseCaseInterfaces
{
    public interface IUpdateProductAsyncUseCase
    {

        Task Execute(int? id, Product product);

    }
}
