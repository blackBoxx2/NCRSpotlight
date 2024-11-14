using Microsoft.AspNetCore.Mvc;

namespace NCRSPOTLIGHT.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
