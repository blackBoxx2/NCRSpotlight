using EntitiesLayer.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Plugins.DataStore.SQLite;

namespace NCRSPOTLIGHT.Controllers
{
    public class RoleController : Controller
    {
        private readonly IdentityContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(IdentityContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {

            var roles = _db.Roles.ToList();


            return View(roles);
        }

        [HttpGet]
        public IActionResult Upsert(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return View();
            }
            else
            {
                var objFromDb = _db.Roles.FirstOrDefault(x => x.Id == roleId);
                return View(objFromDb);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(IdentityRole roleObj)
        {
            //if (!await _roleManager.RoleExistsAsync(roleObj.Name))
            //{
            //    TempData[SD.Error] = $"Error, {roleObj.Name} does not exist";
            //    return View(nameof(Index));
            //}
            if (string.IsNullOrEmpty(roleObj.NormalizedName))
            {
                //create
                await _roleManager.CreateAsync(new IdentityRole() { Name = roleObj.Name });
                TempData[SD.Success] = "Role created successfully";

            }
            else
            {
                //update
                var objFromDb = _db.Roles.FirstOrDefault(x => x.Id == roleObj.Id);
                objFromDb.Name = roleObj.Name;
                objFromDb.NormalizedName = roleObj.NormalizedName.ToUpper();
                var result = await _roleManager.UpdateAsync(objFromDb);
                TempData[SD.Success] = "Role updated successfully";

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "OnlySuperAdminChecker")]
        public async Task<IActionResult> Delete(string roleId)
        {


            var objFromDb = _db.Roles.FirstOrDefault(x => x.Id == roleId);
            if (objFromDb != null)
            {
                var userRolesForThisRole = _db.UserRoles.Where(u => u.RoleId == roleId).Count();
                if (userRolesForThisRole > 0)
                {
                    TempData[SD.Error] = "Cannot delete this role, since there are users assigned to this role.";
                    return RedirectToAction(nameof(Index));

                }

                var result = await _roleManager.DeleteAsync(objFromDb);
                TempData[SD.Success] = "Role deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData[SD.Error] = "Role not found";

            return RedirectToAction(nameof(Index));
        }
    }
}
