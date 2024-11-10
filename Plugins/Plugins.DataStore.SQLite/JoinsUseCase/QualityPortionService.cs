using EntitiesLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.DataStore.SQLite.JoinsUseCase
{
    public class QualityPortionService
    {
        private readonly IdentityContext identityContext;
        private readonly NCRContext nCRContext;

        public QualityPortionService(IdentityContext identityContext, NCRContext nCRContext)
        {
            this.identityContext = identityContext;
            this.nCRContext = nCRContext;
        }

        public async Task<IEnumerable<object>> GetQualityPortionsWithRepresentativeAsync()
        {
            var roles = await identityContext.Roles
                            .Where(x => x.Name == "QualityAssurance")
                            .Select(r => r.Id)
                            .ToListAsync();
            var userRoles = await identityContext.UserRoles
                            .Where(z => roles.Contains(z.RoleId))
                            .Select(x => x.UserId)
                            .ToListAsync();


            var representatives = await identityContext.Users
                                        .Where(x => userRoles.Contains(x.Id))
                                        .ToListAsync();

            var representativesIds = await identityContext.Users
                                        .Where(x => userRoles.Contains(x.Id))
                                        .Select(x => x.Id)
                                        .ToListAsync();
            var qualityPortions = await nCRContext.QualityPortions
                                .Include(qp => qp.Product)  // Edit if need Be ******
                                .Where(qp => representativesIds.Contains(qp.RepId))
                                .ToListAsync();


            var qpNcrJoinTable = from q in qualityPortions
                                 join r in representatives on q.RepId equals r.Id
                                 select new QualityPortionsWithRepViewModel
                                 {
                                     QualityPortion = q,
                                     Representative = r
                                 };

            return qpNcrJoinTable;
        }
    }
}
