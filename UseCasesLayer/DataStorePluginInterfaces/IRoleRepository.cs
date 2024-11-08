using EntitiesLayer.Models;

namespace UseCasesLayer.DataStorePluginInterfaces
{
    public interface IRoleRepository
    {
        Task AddRoleAsync(Role role);
        Task DeleteRoleAsync(int id);
        Task<Role> GetRoleByIDAsync(int? ID);
        Task<IEnumerable<Role>> GetRolesAsync();
        Task UpdateRoleAsync(int? id, Role role);
    }
}