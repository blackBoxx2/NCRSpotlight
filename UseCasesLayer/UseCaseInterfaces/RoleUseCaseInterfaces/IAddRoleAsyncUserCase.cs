using EntitiesLayer.Models;

namespace UseCasesLayer.UseCaseInterfaces.RoleUseCaseInterfaces
{
    public interface IAddRoleAsyncUserCase
    {
        Task Execute(Role Role);
    }
}
