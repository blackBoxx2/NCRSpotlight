using EntitiesLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace UseCasesLayer.UseCaseInterfaces.RoleUseCaseInterfaces
{
    public interface IAddRoleAsyncUserCase
    {
        Task Execute(IdentityRole Role);
    }
}
