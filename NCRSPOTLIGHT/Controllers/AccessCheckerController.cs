using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NCRSPOTLIGHT.Controllers
{
    [Authorize]
    public class AccessCheckerController : Controller
    {
        [AllowAnonymous]
        public IActionResult AllAccess()
        {
            return View();
        }
        public IActionResult AuthorizedAccess()
        {
            return View();
        }
        [Authorize(Roles = $"{SD.Admin}, {SD.User}")]
        public IActionResult UserORAdminRoleAccess()
        {
            return View();
        }
        [Authorize(Policy = "AdminAndUser")]
        public IActionResult UserANDAdminRoleAccess()
        {
            return View();
        }
        [Authorize(Roles = SD.Admin)]
        public IActionResult AdminRoleAccess()
        {
            return View();
        }
        [Authorize(Policy = "AdminRole-CreateClaim")]
        public IActionResult Admin_CreateAccess()
        {
            return View();
        }

        [Authorize(Policy = "AdminRole-CreateEditDeleteClaim")]
        public IActionResult Admin_Create_Edit_DeleteAccess()
        {
            return View();
        }

        [Authorize(Policy = "Admin_Create_Edit_DeleteAccess_OR_SuperAdminRole")]
        public IActionResult Admin_Create_Edit_DeleteAccess_OR_SuperAdminRole()
        {
            return View();
        }

    }
}
