using EntitiesLayer.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQLite;
using System.Security.Claims;
using System.Xml.Linq;

namespace NCRSPOTLIGHT.Controllers
{
    public class UserController : Controller
    {
        private readonly IdentityContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IdentityContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var userList = _db.ApplicationUsers.ToList();
            var userRole = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in userList)
            {
                var userrole = await _userManager.GetRolesAsync(user) as List<string>;

                user.Role = string.Join(", ", userrole);

                var userClaim = _userManager.GetClaimsAsync(user).GetAwaiter().GetResult().Select(u => u.Type);

                user.UseClaim = string.Join(", ", userClaim);
            }
            return View(userList);
        }

        public async Task<IActionResult> ManageRole(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            List<string> existingUserRoles = await _userManager.GetRolesAsync(user) as List<string>;
            var model = new RolesViewModel()
            {
                User = user,
            };
            foreach (var role in _roleManager.Roles)
            {
                RoleSelection roleSelection = new()
                {
                    RoleName = role.Name
                };
                if (existingUserRoles.Any(c => c == role.Name))
                {
                    roleSelection.IsSelected = true;
                }
                model.RolesList.Add(roleSelection);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRole(RolesViewModel rolesViewModel)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(rolesViewModel.User.Id);
            if (user == null)
            {
                return NotFound();
            }
            var oldUserRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, oldUserRoles);
            if (!result.Succeeded)
            {
                TempData[SD.Error] = "Error while remove roles";
                return View(rolesViewModel);
            }
            result = await _userManager.AddToRolesAsync(user,
                rolesViewModel.RolesList.Where(r => r.IsSelected == true).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                TempData[SD.Error] = "Error while remove roles";
                return View(rolesViewModel);
            }
            TempData[SD.Success] = "Roles assigned succesfully";

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> ManageClaim(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var existingUserClaims = await _userManager.GetClaimsAsync(user);
            var model = new ClaimsViewModel()
            {
                User = user,
            };
            foreach (Claim claim in ClaimStore.claimList)
            {
                ClaimSelection claimSelection = new()
                {
                    ClaimType = claim.Type
                };
                if (existingUserClaims.Any(c => c.Type == claim.Type))
                {
                    claimSelection.IsSelected = true;
                }
                model.ClaimList.Add(claimSelection);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageClaim(ClaimsViewModel claimsViewModel)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(claimsViewModel.User.Id);
            if (user == null)
            {
                return NotFound();
            }
            var oldUserClaims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, oldUserClaims);
            if (!result.Succeeded)
            {
                TempData[SD.Error] = "Error while removing claims";
                return View(claimsViewModel);
            }
            result = await _userManager.AddClaimsAsync(user,
                claimsViewModel.ClaimList.Where(r => r.IsSelected == true).Select(y => new Claim(y.ClaimType, y.IsSelected.ToString())));

            if (!result.Succeeded)
            {
                TempData[SD.Error] = "Error while adding claims";
                return View(claimsViewModel);
            }
            TempData[SD.Success] = "Claims assigned succesfully";

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LockUnlock(string userId)
        {
            ApplicationUser user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
                TempData[SD.Success] = "User unlocked successfully";
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
                TempData[SD.Success] = "User locked successfully";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            ApplicationUser user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                TempData[SD.Error] = "Error, that user doesnt exist";
                return NotFound();
            }
            else
            {
                TempData[SD.Success] = $"Success {user.FirstName} {user.LastName} was deleted";
                _db.ApplicationUsers.Remove(user);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            ApplicationUser user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                TempData[SD.Error] = "Error, that user doesnt exist";
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(ApplicationUser user)
        {
            ApplicationUser userFromDb = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == user.Id);
            if (user == null)
            {
                TempData[SD.Error] = "Error, that user doesnt exist";
                return NotFound();
            }
            else
            {
                userFromDb.FirstName = user.FirstName;
                userFromDb.LastName = user.LastName;
                userFromDb.PhoneNumber = user.PhoneNumber;
                userFromDb.Email = user.Email;
                user.LockoutEnabled = user.LockoutEnabled;
                TempData[SD.Success] = $"Success {user.FirstName} {user.LastName} was updated";
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }



    }
}
