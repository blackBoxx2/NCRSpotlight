﻿using System;
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
