using EntitiesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCasesLayer.DataStorePluginInterfaces
{
    public interface IRoleRepRepository
    {
        Task<RoleRep> GetRoleRepByIdAsync(int? id);
        Task AddRoleRepAsync(RoleRep roleRep);
        Task UpdateRoleRepAsync(int? id, RoleRep roleRep);
        Task<IEnumerable<RoleRep>> GetRoleRepASync();
        Task DeleteRoleRepAsync(int id);
    }
}
