using EntitiesLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace UseCasesLayer.DataStorePluginInterfaces
{
    public interface IRoleRepository
    {
        Task AddRoleAsync(IdentityRole role);
        Task DeleteRoleAsync(string id);
        Task<IdentityRole> GetRoleByIDAsync(string ID);
        Task<IEnumerable<IdentityRole>> GetRolesAsync();
        Task UpdateRoleAsync(string id, IdentityRole role);
    }
}