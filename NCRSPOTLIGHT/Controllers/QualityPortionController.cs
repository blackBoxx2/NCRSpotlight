﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntitiesLayer.Models;
using Plugins.DataStore.SQLite;
using UseCasesLayer.UseCaseInterfaces.QualityPortionUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.QualityPortionUseCase;
using UseCasesLayer.UseCaseInterfaces.ProductUseCaseInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NCRSPOTLIGHT.Controllers
{
    public class QualityPortionController : Controller
    {
        private readonly IAddQualityPortionAsyncUseCase _addQualityPortionAsyncUseCase;
        private readonly IDeleteQualityPortionAsyncUseCase _deleteQualityPortionAsyncUseCase;
        private readonly IGetQualityPortionsAsyncUseCase _getQualityPortionsAsyncUseCase;
        private readonly IGetQualityPortionByIDAsyncUseCase _getQualityPortionByIDAsyncUseCase;
        private readonly IUpdateQualityPortionAsyncUseCase _updateQualityPortionAsyncUseCase;
        private readonly IGetProductsAsyncUseCase _getProductsAsyncUseCase;



        public QualityPortionController(IAddQualityPortionAsyncUseCase addQualityPortionAsyncUseCase,
                                        IDeleteQualityPortionAsyncUseCase deleteQualityPortionAsyncUseCase,
                                        IGetQualityPortionsAsyncUseCase getQualityPortionsAsyncUseCase,
                                        IGetQualityPortionByIDAsyncUseCase getQualityPortionByIDAsyncUseCase,
                                        IUpdateQualityPortionAsyncUseCase updateQualityPortionAsyncUseCase,
                                        IGetProductsAsyncUseCase getProductsAsyncUseCase
                                        )
        {
            _addQualityPortionAsyncUseCase = addQualityPortionAsyncUseCase;
            _deleteQualityPortionAsyncUseCase = deleteQualityPortionAsyncUseCase;
            _getQualityPortionByIDAsyncUseCase = getQualityPortionByIDAsyncUseCase;
            _getQualityPortionsAsyncUseCase = getQualityPortionsAsyncUseCase;
            _updateQualityPortionAsyncUseCase = updateQualityPortionAsyncUseCase;
            _getProductsAsyncUseCase = getProductsAsyncUseCase;

        }

        // GET: QualityPortion
        public async Task<IActionResult> Index(int? ProductID, string? RepId)
        {
            var qualityPortions = await _getQualityPortionsAsyncUseCase.Execute();
            LoadSelectList(new QualityPortion());
           
            return View(qualityPortions);
        }

        // GET: QualityPortion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualityPortion = await _getQualityPortionByIDAsyncUseCase.Execute(id);
            if (qualityPortion == null)
            {
                return NotFound();
            }

            return View(qualityPortion);
        }

        // GET: QualityPortion/Create
        public async Task<IActionResult> Create()
        {
            var qualityPortion = new QualityPortion();            
            ViewData["User"] = HttpContext.User.Identity.Name;
            LoadSelectList(qualityPortion);
            return View(qualityPortion);
        }

        // POST: QualityPortion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QualityPortion qualityPortion, List<IFormFile> theFiles)
        {

            if (ModelState.IsValid)
            {
                await AddDocumentsAsync(qualityPortion, theFiles);
                await _addQualityPortionAsyncUseCase.Execute(qualityPortion);
                return RedirectToAction(nameof(Index));
            }
            LoadSelectList(qualityPortion);
            return View(qualityPortion);
        }

        // GET: QualityPortion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualityPortion = await _getQualityPortionByIDAsyncUseCase.Execute(id);


            if (qualityPortion == null)
            {
                return NotFound();
            }
            LoadSelectList(qualityPortion);
            return View(qualityPortion);
        }

        // POST: QualityPortion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductID,Quantity,QuantityDefective,OrderNumber,DefectDescription,RepID")] QualityPortion qualityPortion, List<IFormFile> theFiles)
        {

            qualityPortion.qualityPictures = _getQualityPortionByIDAsyncUseCase.Execute(id).Result.qualityPictures;

            if (id != qualityPortion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await AddDocumentsAsync(qualityPortion, theFiles);
                    await _updateQualityPortionAsyncUseCase.Execute(id, qualityPortion);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await QualityPortionExists(qualityPortion.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            LoadSelectList(qualityPortion);
            return View(qualityPortion);
        }

        // GET: QualityPortion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualityPortion = await _getQualityPortionByIDAsyncUseCase.Execute(id);

            if (qualityPortion == null)
            {
                return NotFound();
            }

            return View(qualityPortion);
        }

        // POST: QualityPortion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var qualityPortion = await _getQualityPortionByIDAsyncUseCase.Execute(id);


            if (qualityPortion != null)
            {
                await _deleteQualityPortionAsyncUseCase.Execute(qualityPortion.ID);
            }





            return RedirectToAction(nameof(Index));
        }
        public async void LoadSelectList(QualityPortion qualityPortion)
        {

            ViewBag.ProductID = new SelectList(await _getProductsAsyncUseCase.Execute(), "ID", "Description", qualityPortion.ProductID);
            if (ViewBag.ProductId == null)
            {
                throw new Exception(" List is Empty");
            }
        }

        private async Task<bool> QualityPortionExists(int id)
        {
            var portion = await _getQualityPortionByIDAsyncUseCase.Execute(id);
            return portion != null;
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
