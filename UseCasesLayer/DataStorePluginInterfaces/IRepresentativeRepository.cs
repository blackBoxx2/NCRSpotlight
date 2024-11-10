using EntitiesLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace UseCasesLayer.DataStorePluginInterfaces
{
    public interface IRepresentativeRepository
    {
        Task AddRepresentativeAsync(IdentityUser representative);
        Task DeleteRepresentativeAsync(string id);
        Task<IEnumerable<IdentityUser>> GetRepresentativesAsync();
        Task<IdentityUser> GetRepresentativesByIdAsync(string id);
        Task UpdateRepresentativeAsync(string id, IdentityUser representative);
    }
}