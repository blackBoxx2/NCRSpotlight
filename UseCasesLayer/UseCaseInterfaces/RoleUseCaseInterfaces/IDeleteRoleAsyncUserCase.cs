using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.UseCaseInterfaces.RoleUseCaseInterfaces
{
    public interface IDeleteRoleAsyncUserCase
    {
        Task Execute(string id);
    }
}
