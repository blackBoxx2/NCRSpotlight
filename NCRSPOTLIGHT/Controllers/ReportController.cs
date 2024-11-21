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
            var qualityPortions =  ncrLogs.Select(n => n.QualityPortion).ToList(); 
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
            })
             .OrderByDescending(v => v.y)
                .ToList();
            return Json(dataPoints);
        }

        public async Task<IActionResult> Top4MostBrokenProducts()
        {
            var ncrs = await nCRContext.NCRLog
                .Include(n => n.QualityPortion)
                .ThenInclude(qp => qp.Product)
                .ThenInclude(p => p.Supplier)
                .ToListAsync();

            var products = ncrs.Select(q => q.QualityPortion.Product.Description).ToList();

            var fourMostCommonProducts = new Dictionary<string, int>();

            foreach(var ncr in ncrs)
            {
                if(ncr.QualityPortion.Product.Description !=null)
                {
                    if(fourMostCommonProducts.ContainsKey(ncr.QualityPortion.Product.Description))
                    {
                        fourMostCommonProducts[ncr.QualityPortion.Product.Description]++;
                    }
                    else
                    {
                        fourMostCommonProducts[ncr.QualityPortion.Product.Description] = 1;
                    }
                }
            }

			var dataPoints = fourMostCommonProducts.Select(kvp => new
			{
				Label = kvp.Key,
				y = kvp.Value
			})
            .OrderByDescending(v => v.y)
            .Take(4)
	        .ToList();
			return Json(dataPoints);
		}

        public async Task<IActionResult> NumberOfNCROverTime()
        {
            var ncrs = await nCRContext.NCRLog
                .Include(n => n.QualityPortion)
                .ThenInclude(qp => qp.Product)
                .ThenInclude(p => p.Supplier)
                .ToListAsync();
            var months = Enumerable.Range(1, 2)
                 .Select(month => new DateTime(2024, month, 1)
                 .ToString("MMMM"))
                 .ToList();

            var ncrInMonth = new Dictionary<string, int>();
            foreach(var ncr in ncrs)
            {
                if(ncr.DateCreated != DateTime.MinValue)
                {
                    if(ncrInMonth.ContainsKey(ncr.DateCreated.ToString("MMMM")))
                    {
                        ncrInMonth[ncr.DateCreated.ToString("MMMM")]++;
                    }
                    else
                    {
						ncrInMonth[ncr.DateCreated.ToString("MMMM")] =1;

					}
				}

            }
			var dataPoints = ncrInMonth.Select(kvp => new
			{
				Label = kvp.Key,
				y = kvp.Value
			})
            .ToList();
			return Json(dataPoints);
		}
    }
}
