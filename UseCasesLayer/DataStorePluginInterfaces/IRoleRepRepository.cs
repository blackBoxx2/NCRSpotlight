using EntitiesLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.DataStorePluginInterfaces
{
    public interface IRoleRepRepository
    {
        Task<IdentityUserRole<string>> GetRoleRepByIdAsync(string id);
        Task AddRoleRepAsync(IdentityUserRole<string> roleRep);
        Task UpdateRoleRepAsync(string id, IdentityUserRole<string> roleRep);
        Task<IEnumerable<IdentityUserRole<string>>> GetRoleRepASync();
        Task DeleteRoleRepAsync(string id);
    }
}
