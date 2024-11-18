using EntitiesLayer.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace NCRSPOTLIGHT.Utilities
{
    public class RoleChecker
    {
        private readonly UserManager<ApplicationUser> userManager;

        public RoleChecker(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> CanEdit(ClaimsPrincipal user)
        {
            var currUser = await userManager.GetUserAsync(user);
            return await userManager.IsInRoleAsync(currUser, SD.QualityAssurance)||
                await userManager.IsInRoleAsync(currUser, SD.Admin)||
                await userManager.IsInRoleAsync(currUser, SD.SuperAdmin);
        }
    }
}
