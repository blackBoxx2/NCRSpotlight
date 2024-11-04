using EntitiesLayer.Models;

namespace Plugins.DataStore.SQLite
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