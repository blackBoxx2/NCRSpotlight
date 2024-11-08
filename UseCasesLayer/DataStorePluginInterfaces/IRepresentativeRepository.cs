using EntitiesLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace UseCasesLayer.DataStorePluginInterfaces
{
    public interface IRepresentativeRepository
    {
        Task AddRepresentativeAsync(Representative representative);
        Task DeleteRepresentativeAsync(int id);
        Task<IEnumerable<IdentityUser>> GetRepresentativesAsync();
        Task<Representative> GetRepresentativesByIdAsync(int? id);
        Task UpdateRepresentativeAsync(int? id, Representative representative);
    }
}