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
    public abstract class NotifyingController : Controller
    {
        internal IGetNCRLogsAsyncUseCase getNCRLogsAsyncUseCase;

        public NotifyingController(IGetNCRLogsAsyncUseCase getNCRLogsAsyncUseCase)
        {
            this.getNCRLogsAsyncUseCase = getNCRLogsAsyncUseCase;
        }
        public virtual async void HandleNotifications()
        {
            // --- handle notification

            // defaults
            ViewData["NotificationDate"] = "";
            ViewBag.ShowNotification = false;
            ViewData["NewCount"] = "0";
            // get user
            UserManager<ApplicationUser> userManager = HttpContext.RequestServices.GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;
            var applicationUser = await userManager.GetUserAsync(User);

            // show notification
            if (applicationUser != null)
            {
                // ViewData["LastViewed"] = applicationUser.LastViewedNCRS; // debug, view the last viewed ncr time on the page
                if (applicationUser.LastViewedNCRS != null)
                {
                    // pull all ncrs
                    var ncrLogs = await getNCRLogsAsyncUseCase.Execute();
                    // get the ones that are new
                    var newNCRLogs = ncrLogs.Where(p => p.DateCreated >= applicationUser.LastViewedNCRS.Value.Date);
                    var newCount = newNCRLogs.Count();
                    ViewData["NotificationDate"] = applicationUser.LastViewedNCRS.Value.ToString("yyyy-MM-dd");
                    ViewData["LastViewed"] = applicationUser.LastViewedNCRS.Value.ToString("yyyy-MM-dd HH:mm:ss tt");
                    ViewData["NewCount"] = newCount;
                    // show notification if there are new ncrs
                    if (newCount > 0)
                    {
                        ViewBag.ShowNotification = true;
                    }
                }

            }

        }
    }
}
