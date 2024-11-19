using EntitiesLayer.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NCRSPOTLIGHT.Models;
using Plugins.DataStore.SQLite;
using System.Diagnostics;
using UseCasesLayer.UseCaseInterfaces.NCRLogUseCaseInterfaces;

namespace NCRSPOTLIGHT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IGetNCRLogsAsyncUseCase getNCRLogsAsyncUseCase;

        public HomeController(
            ILogger<HomeController> logger, 
            UserManager<ApplicationUser> userManager,
            IGetNCRLogsAsyncUseCase getNCRLogsAsyncUseCase
        )
        {
            _logger = logger;
            this._userManager = userManager;
            this.getNCRLogsAsyncUseCase = getNCRLogsAsyncUseCase;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                ViewData["TwoFactorEnabled"] = false;
            }
            else
            {
                ViewData["TwoFactorEnabled"] = user.TwoFactorEnabled;
            }
            // --- handle notification

            // defaults
            ViewData["NotificationDate"] = "";
            ViewData["ShowNotification"] = "hidden";
            ViewData["NewCount"] = "0";
            // get user
            UserManager<ApplicationUser> userManager = HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;
            var applicationUser = await userManager.GetUserAsync(User);
            
            // show notification
            if (applicationUser != null) {
                // ViewData["LastViewed"] = applicationUser.LastViewedNCRS; // debug, view the last viewed ncr time on the page
                if (applicationUser.LastViewedNCRS != null)
                {
                    // pull all ncrs
                    var ncrLogs = await getNCRLogsAsyncUseCase.Execute();
                    // get the ones that are new
                    var newNCRLogs = ncrLogs.Where(p => p.DateCreated >= applicationUser.LastViewedNCRS.Value.Date);
                    var newCount = newNCRLogs.Count();
                    ViewData["NotificationDate"] = applicationUser.LastViewedNCRS.Value.ToString("yyyy-MM-dd");
                    ViewData["NewCount"] = newCount;
                    // show notification if there are new ncrs
                    if (newCount > 0)
                    {
                        ViewData["ShowNotification"] = "visible";
                    }
                }
                
            }
            

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
