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
    public class HomeController : NotifyingController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;


        public HomeController(
            ILogger<HomeController> logger, 
            UserManager<ApplicationUser> userManager,
            IGetNCRLogsAsyncUseCase getNCRLogsAsyncUseCase

        )
            : base(getNCRLogsAsyncUseCase)
        {
            _logger = logger;
            this._userManager = userManager;
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

            HandleNotifications();
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
