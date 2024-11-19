using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntitiesLayer.Models;
using Plugins.DataStore.SQLite;
using UseCasesLayer.UseCaseInterfaces.NCRLogUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.QualityPortionUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.QualityPortionUseCase;
using Microsoft.AspNetCore.Authorization;
using UseCasesLayer.UseCaseInterfaces.ProductUseCaseInterfaces;
using System.Security.Claims;
using UseCasesLayer.UseCaseInterfaces.EngUseCaseInterface;
using UseCasesLayer.UseCaseInterfaces.SuppliersUseCaseInterfaces;
using System.Reflection.Metadata;
using QuestPDF.Fluent;
using QuestPDF.Previewer;
using QuestPDF.Helpers;

namespace NCRSPOTLIGHT.Controllers
{

    public class NCRLogController : Controller
    {

        private readonly IAddNCRLogAsyncUseCase _addNCRLogAsyncUseCase;
        private readonly IDeleteNCRLogAsyncUseCase _delNCRLogAsyncUseCase;
        private readonly IGetNCRLogByIDAsyncUseCase _getNCRLogByIDAsyncUseCase;
        private readonly IGetNCRLogsAsyncUseCase _getNCRLogsAsyncUseCase;
        private readonly IUpdateNCRLogAsyncUseCase _updateNCRLogAsyncUseCase;
        private readonly IGetQualityPortionsAsyncUseCase _getQualityPortionsAsyncUseCase;
        private readonly IAddQualityPortionAsyncUseCase _addQualityPortionAsyncUseCase;
        private readonly IGetProductsAsyncUseCase _getProductsAsyncUseCase;
        private readonly IUpdateQualityPortionAsyncUseCase _UpdateQualityPortionAsyncUseCase;
        private readonly IGetQualityPortionByIDAsyncUseCase _getQualityPortionByIDAsyncUseCase;
        private readonly IAddEngPortionAsyncUseCase _addEngPortionAsyncUseCase;
        private readonly IGetEngPortionsAsyncUseCase _getEngPortionsAsyncUseCase;
        private readonly IUpdateEngPortionAsyncUseCase _updateEngPortionAsyncUseCase;
        private readonly IGetSuppliersAsyncUseCase _getSuppliersAsyncUseCase;
        private readonly IGetSupplierByIDAsyncUseCase _getSupplierByIDAsyncUseCase;

        public NCRLogController(IAddNCRLogAsyncUseCase addNCRLogAsyncUseCase,
                                IDeleteNCRLogAsyncUseCase deleteNCRLogAsyncUseCase,
                                IGetNCRLogByIDAsyncUseCase getNCRLogByIDAsyncUseCase,
                                IGetNCRLogsAsyncUseCase getNCRLogsAsyncUseCase,
                                IUpdateNCRLogAsyncUseCase updateNCRLogAsyncUseCase,
                                IGetQualityPortionsAsyncUseCase getQualityPortionsAsyncUseCase,
                                IAddQualityPortionAsyncUseCase addQualityPortionAsyncUseCase,
                                IGetProductsAsyncUseCase getProductsAsyncUseCase,
                                IUpdateQualityPortionAsyncUseCase updateQualityPortionAsyncUseCase,
                                IGetQualityPortionByIDAsyncUseCase getQualityPortionByIDAsyncUseCase,
                                IAddEngPortionAsyncUseCase addEngPortionAsyncUseCase,
                                IGetEngPortionsAsyncUseCase getEngPortionsAsyncUseCase,
                                IUpdateEngPortionAsyncUseCase updateEngPortionAsyncUseCase,
                                IGetSuppliersAsyncUseCase getSuppliersAsyncUseCase,
                                IGetSupplierByIDAsyncUseCase getSupplierByIDAsyncUseCase
                                )
        {
            _addNCRLogAsyncUseCase = addNCRLogAsyncUseCase;
            _delNCRLogAsyncUseCase = deleteNCRLogAsyncUseCase;
            _getNCRLogByIDAsyncUseCase = getNCRLogByIDAsyncUseCase;
            _getNCRLogsAsyncUseCase = getNCRLogsAsyncUseCase;
            _updateNCRLogAsyncUseCase = updateNCRLogAsyncUseCase;
            _getQualityPortionsAsyncUseCase = getQualityPortionsAsyncUseCase;
            _addQualityPortionAsyncUseCase = addQualityPortionAsyncUseCase;
            _getProductsAsyncUseCase = getProductsAsyncUseCase;
            _UpdateQualityPortionAsyncUseCase = updateQualityPortionAsyncUseCase;
            _getQualityPortionByIDAsyncUseCase = getQualityPortionByIDAsyncUseCase;
            _addEngPortionAsyncUseCase = addEngPortionAsyncUseCase;
            _getEngPortionsAsyncUseCase = getEngPortionsAsyncUseCase;
            _updateEngPortionAsyncUseCase = updateEngPortionAsyncUseCase;
            _getSuppliersAsyncUseCase = getSuppliersAsyncUseCase;
            _getSupplierByIDAsyncUseCase = getSupplierByIDAsyncUseCase;

        }

        // GET: NCRLog
        public async Task<IActionResult> Index(DateTime StartDate, DateTime EndDate, string? AutoFilterDate = null)
        {

            var nCRContext = await _getNCRLogsAsyncUseCase.Execute();

            // first time startup
            if (EndDate == DateTime.MinValue)
            {
                StartDate = nCRContext.Min(p => p.DateCreated).Date;
                EndDate = nCRContext.Max(p => p.DateCreated).Date;
            }
            // edge case protection
            if (EndDate < StartDate)
            {
                DateTime temp = EndDate;
                EndDate = StartDate;
                StartDate = temp;
            }

            if (AutoFilterDate != null)
            {
                StartDate = DateTime.Parse(AutoFilterDate);
            }


            // set boxes
            ViewData["StartDate"] = StartDate.ToString("yyyy-MM-dd");
            ViewData["EndDate"] = EndDate.ToString("yyyy-MM-dd");

            nCRContext = nCRContext.Where(p=> p.DateCreated >= StartDate && p.DateCreated <= EndDate.AddDays(1));
            return View(nCRContext);
        }

        // GET: NCRLog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nCRLog = await _getNCRLogByIDAsyncUseCase.Execute(id);
            if (nCRLog == null)
            {
                return NotFound();
            }

            return View(nCRLog);
        }

        // GET: NCRLog/Create
        public async Task<IActionResult> Create()
        {
            ViewData["User"] = HttpContext.User.Identity.Name;
            ViewData["NCRNumber"] = _getNCRLogsAsyncUseCase.Execute().Result.Last().ID + 1;
            LoadSelectList(new NCRLog());
        
            var user = HttpContext.User;                      

            return View();
        }

        // POST: NCRLog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DateCreated,Status")] NCRLog nCRLog, 
            [Bind("ProductID,Quantity,QuantityDefective,OrderNumber,DefectDescription,ProcessApplicable,RepID,Created")] QualityPortion qualityPortion, 
            [Bind("EngReview,Disposition,Update,Notif,RevNumber,RevDate,RepID,OriginalEngineer,OriginalRevNumber,Date")] EngPortion engPortion,
            List<IFormFile> theFiles)
        {
            
            if(theFiles.Count > 0)
            {
                await AddDocumentsAsync(qualityPortion, theFiles);
            }

            await _addQualityPortionAsyncUseCase.Execute(qualityPortion);
            await _addEngPortionAsyncUseCase.Execute(engPortion);
            nCRLog.QualityPortionID = _getQualityPortionsAsyncUseCase.Execute().Result.Last().ID;
            nCRLog.EngPortionID = _getEngPortionsAsyncUseCase.Execute().Result.Last().ID; 


            try
            {
                if (ModelState.IsValid)
                {
                    await _addNCRLogAsyncUseCase.Execute(nCRLog);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("DateCreated", ex.GetBaseException().ToString());
            
            }
            ViewData["QualityPortionID"] = new SelectList(await _getQualityPortionsAsyncUseCase.Execute(), "ID", "DefectDescription", nCRLog.QualityPortionID);
            return View(nCRLog);
        }

        // GET: NCRLog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
    
            var nCRLog = await _getNCRLogByIDAsyncUseCase.Execute(id);
            if (nCRLog == null)
            {
                return NotFound();
            }

            var user = HttpContext.User;

            LoadSelectList(nCRLog);
            return View(nCRLog);
        }

        // POST: NCRLog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ID, int QualityPortionID, int EngPortionID, [Bind("ID,QualityPortionID,DateCreated,Status")] NCRLog nCRLog, 
            [Bind("ProductID,Quantity,QuantityDefective,OrderNumber,DefectDescription,ProcessApplicable,RepID,Created")] QualityPortion qualityPortion, 
            [Bind("EngReview,Disposition,Update,Notif,RevNumber,RevDate,RepID,OriginalEngineer,OriginalRevNumber,Date")] EngPortion engPortion,
            List<IFormFile> theFiles)
        {
            if (ID != nCRLog.ID)
            {
                return NotFound();
            }

            qualityPortion.qualityPictures = _getQualityPortionByIDAsyncUseCase.Execute(QualityPortionID).Result.qualityPictures;
            qualityPortion.ID = QualityPortionID;
            engPortion.ID = EngPortionID;

            if (ModelState.IsValid)
            {
                try
                {
                    await AddDocumentsAsync(qualityPortion, theFiles);
                    await _updateEngPortionAsyncUseCase.Execute(EngPortionID, engPortion);
                    await _UpdateQualityPortionAsyncUseCase.Execute(nCRLog.QualityPortionID, qualityPortion);
                    await _updateNCRLogAsyncUseCase.Execute(ID, nCRLog);                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NCRLogExists(nCRLog.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { nCRLog.ID });
            }
            LoadSelectList(nCRLog);
            return View(nCRLog);
        }

        // GET: NCRLog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nCRLog = await _getNCRLogByIDAsyncUseCase.Execute(id);
            if (nCRLog == null)
            {
                return NotFound();
            }

            return View(nCRLog);
        }

        // POST: NCRLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nCRLog = await _getNCRLogByIDAsyncUseCase.Execute(id);
            if (nCRLog != null)
            {
                await _delNCRLogAsyncUseCase.Execute(nCRLog.ID);
            }
          
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GeneratePDF(int ID)
        {

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            var Log = await _getNCRLogByIDAsyncUseCase.Execute(ID);

            var doc = QuestPDF.Fluent.Document.Create(container =>
            {

                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);
                    page.Header().Border(1).PaddingHorizontal(10).BorderColor(Colors.Grey.Medium)
                    .Row(row =>
                    {

                        row.AutoItem().PaddingVertical(5).Text("CrossFire Canada").FontSize(20);
                        row.AutoItem().PaddingHorizontal(10).LineVertical(1).LineColor(Colors.Grey.Medium);
                        row.AutoItem().PaddingVertical(5).AlignBottom().Text("Internal Process Document");

                    });

                    page.Content().BorderVertical(1).Column(column =>
                    {

                        column.Item().BorderHorizontal(1).PaddingHorizontal(10).BorderColor(Colors.Grey.Medium).Row(row =>
                        {
                            row.AutoItem().PaddingVertical(5).Text($"Document Number: OPS-00011");
                            row.AutoItem().Height(25).PaddingHorizontal(10).LineVertical(1).LineColor(Colors.Grey.Medium);
                            row.AutoItem().PaddingVertical(5).Text("Document Name: Non-Conformance Report");
                        });
                        column.Item().BorderHorizontal(1).PaddingHorizontal(10).BorderColor(Colors.Grey.Medium).Row(row =>
                        {
                            row.AutoItem().PaddingVertical(5).Text($"NCR Number: {Log.ID}");
                            row.AutoItem().Height(25).PaddingHorizontal(10).LineVertical(1).LineColor(Colors.Grey.Medium);
                            if (Log.QualityPortion.ProcessApplicable.ToString() == "Supplier")
                            {
                                row.AutoItem().PaddingVertical(5).Text($"Process Applicable: Supplier or Rec-Insp");
                            }
                            else
                            {
                                row.AutoItem().PaddingVertical(5).Text($"Process Applicable: WIP (Production Order)");
                            }

                        });

                        column.Item().PaddingHorizontal(10).BorderColor(Colors.Grey.Medium).Row(row => {

                            row.AutoItem().PaddingTop(5).Width(300).Text("Description of Item (including SAP No.):");
                            row.AutoItem().Height(20).PaddingHorizontal(10).LineVertical(1).LineColor(Colors.Grey.Medium);
                            row.AutoItem().PaddingTop(5).Text("PO or Prod. NO.");

                        });

                        column.Item().BorderBottom(1).PaddingHorizontal(10).BorderColor(Colors.Grey.Medium).Row(row =>
                        {

                            row.AutoItem().Width(300).Text(Log.QualityPortion.Product.Summary);
                            row.AutoItem().Height(20).PaddingHorizontal(10).LineVertical(1).LineColor(Colors.Grey.Medium);
                            row.AutoItem().Text(Log.QualityPortion.Product.ProductNumber);

                        });

                        column.Item().BorderHorizontal(1).PaddingHorizontal(10).BorderColor(Colors.Grey.Medium).Row(row =>
                        {
                            row.AutoItem().PaddingVertical(5).Text($"Sales Order No. {Log.QualityPortion.OrderNumber}");
                            row.AutoItem().Height(25).PaddingHorizontal(10).LineVertical(1).LineColor(Colors.Grey.Medium);
                            row.AutoItem().PaddingVertical(5).Text($"Quantity Ordered: {Log.QualityPortion.Quantity}");
                            row.AutoItem().Height(25).PaddingHorizontal(10).LineVertical(1).LineColor(Colors.Grey.Medium);
                            row.AutoItem().PaddingVertical(5).Text($"Quantity Defective: {Log.QualityPortion.QuantityDefective}");

                        });

                        column.Item().PaddingHorizontal(10).BorderColor(Colors.Grey.Medium).Row(row =>
                        {

                            row.AutoItem().PaddingTop(5).Width(300).Text("Description of Defect: (in as much detail as possible)");

                        });

                        column.Item().BorderBottom(1).PaddingHorizontal(10).PaddingBottom(5).Text(Log.QualityPortion.DefectDescription);

                        column.Item().BorderHorizontal(1).PaddingHorizontal(10).BorderColor(Colors.Grey.Medium).Row(row =>
                        {

                            row.AutoItem().PaddingTop(5).Text("Item Non-Conforming: Yes");
                            row.AutoItem().PaddingHorizontal(10).LineVertical(1).LineColor(Colors.Grey.Medium);
                            row.AutoItem().PaddingVertical(5).Text($"Date: {Log.QualityPortion.Created}");

                        });

                        column.Item().BorderHorizontal(1).PaddingHorizontal(10).BorderColor(Colors.Grey.Medium).Row(row =>
                        {

                            row.AutoItem().PaddingVertical(5).Text($"QA Rep: {Log.QualityPortion.RepID}");

                        });


                    });

                });

            });

            doc.GeneratePdfAndShow();

            return RedirectToAction("Details", new { Log.ID });


        }

        public async void LoadSelectList(NCRLog log)
        {
            if(log.QualityPortion != null)
            {
                ViewBag.ProductID = new SelectList(await _getProductsAsyncUseCase.Execute(), "ID", "Summary", log.QualityPortion.ProductID);
                ViewBag.SupplierID = new SelectList(await _getProductsAsyncUseCase.Execute(), "ID", "Supplier.SupplierName", log.QualityPortion.ProductID);
                ViewBag.ProdNumber = new SelectList(await _getProductsAsyncUseCase.Execute(), "ID", "ProductNumber", log.QualityPortion.ProductID);
            }
            else
            {
                ViewBag.ProductID = new SelectList(await _getProductsAsyncUseCase.Execute(), "ID", "Summary");
                ViewBag.SupplierID = new SelectList(await _getProductsAsyncUseCase.Execute(), "ID", "Supplier.SupplierName");
                ViewBag.ProdNumber = new SelectList(await _getProductsAsyncUseCase.Execute(), "ID", "ProductNumber");
            }
            
        }
        private bool NCRLogExists(int id)
        {
            var log = _getNCRLogByIDAsyncUseCase.Execute(id);
            return log != null;
        }
        private async Task AddDocumentsAsync(QualityPortion qualityPortion, List<IFormFile> theFiles)
        {

            foreach (var f in theFiles)
            {
                if (f != null)
                {
                    string mimeType = f.ContentType;
                    string fileName = Path.GetFileName(f.FileName);
                    long fileLength = f.Length;
                    if (!(fileName == "" || fileLength == 0))
                    {
                        QualityPicture p = new QualityPicture();
                        using (var memoryStream = new MemoryStream())
                        {
                            await f.CopyToAsync(memoryStream);

                            p.FileContent.Content = memoryStream.ToArray();

                        }
                        p.MimeType = mimeType;
                        p.FileName = fileName;
                        qualityPortion.qualityPictures.Add(p);
                    };
                }
            }
        }

    }
}
