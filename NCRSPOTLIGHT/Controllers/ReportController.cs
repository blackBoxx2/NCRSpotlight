using EntitiesLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQLite;
using System.Reflection.Emit;

namespace NCRSPOTLIGHT.Controllers
{
    public class ReportController : Controller
    {
		private readonly NCRContext nCRContext;

		public ReportController( NCRContext nCRContext)
        {
			this.nCRContext = nCRContext;
		}
        public IActionResult Index()
        {
            var fiveRecentNCRs = nCRContext.NCRLog
                .Include(p => p.QualityPortion)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.Supplier)
                .Where(p => p.Status == NCRStatus.Active)
                .OrderByDescending(p => p.DateCreated)
                .Take(5);
            return View(fiveRecentNCRs);
        }

        public async Task<IActionResult> NCRBySupplier()
        {
            var suppliers = await nCRContext.Suppliers.Select(s => s.SupplierName).ToListAsync();
            var ncrLogs = await nCRContext.NCRLog
                .Include(n => n.QualityPortion)
                .ThenInclude(qp => qp.Product)
                .ThenInclude(p => p.Supplier)
                .ToListAsync();
            var qualityPortions =  ncrLogs.Select(n => n.QualityPortion)
                .ToList(); 
            var products =  qualityPortions.Select(p => p.Product).ToList();
            var ncrLog = new Dictionary<string, int>();
            foreach(var ncr in ncrLogs)
            {
                if (ncr.QualityPortion.Product.Supplier.SupplierName != null)
                {
                    if (ncrLog.ContainsKey(ncr.QualityPortion.Product.Supplier.SupplierName))
                    {
                        ncrLog[ncr.QualityPortion.Product.Supplier.SupplierName]++;
                    }
                    else
                    {
                        ncrLog[ncr.QualityPortion.Product.Supplier.SupplierName] = 1; ;
					}
                }
            }
            var dataPoints = ncrLog.Select(kvp => new
            {
                Label = kvp.Key,
                y = kvp.Value
            }).ToList();
            return Json(dataPoints);
        }
    }
}
