using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.UseCaseInterfaces.RoleRepUseCaseInterfaces
{
    public interface IDeleteRoleRepAsyncUseCase
    {
        Task Execute(int id);
    }
}
