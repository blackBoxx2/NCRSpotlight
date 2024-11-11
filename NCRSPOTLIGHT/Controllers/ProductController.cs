using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntitiesLayer.Models;
using Plugins.DataStore.SQLite;
using UseCasesLayer.UseCaseInterfaces.ProductUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.SuppliersUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.SuppliersUseCases;
using System.Numerics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Humanizer.Bytes;
using Microsoft.AspNetCore.Authorization;

namespace NCRSPOTLIGHT.Controllers
{
    public class ProductController : Controller
    {

        private readonly IAddProductAsyncUseCase _addProductAsyncUseCase;
        private readonly IDeleteProductAsyncUseCase _delProductAsyncUseCase;
        private readonly IGetProductByIDAsyncUseCase _getProductByIDAsyncUseCase;
        private readonly IGetProductsAsyncUseCase _getProductsAsyncUseCase;
        private readonly IUpdateProductAsyncUseCase _updateProductAsyncUseCase;
        private readonly IGetSuppliersAsyncUseCase _getSuppliersAsyncUseCase;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IAddProductAsyncUseCase addProductAsyncUseCase, 
                                 IDeleteProductAsyncUseCase deleteProductAsyncUseCase,
                                 IGetProductByIDAsyncUseCase getProductByIDAsyncUseCase,
                                 IGetProductsAsyncUseCase getProductsAsyncUseCase,
                                 IUpdateProductAsyncUseCase updateProductAsyncUseCase,
                                 IGetSuppliersAsyncUseCase getSuppliersAsyncUseCase,
                                 IWebHostEnvironment webHostEnvironment)
        {
            _addProductAsyncUseCase = addProductAsyncUseCase;
            _delProductAsyncUseCase = deleteProductAsyncUseCase;
            _getProductByIDAsyncUseCase = getProductByIDAsyncUseCase;
            _getProductsAsyncUseCase = getProductsAsyncUseCase;
            _updateProductAsyncUseCase = updateProductAsyncUseCase;
            _getSuppliersAsyncUseCase = getSuppliersAsyncUseCase;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var NCRContext = await _getProductsAsyncUseCase.Execute();
            return View(NCRContext);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _getProductByIDAsyncUseCase.Execute(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            ViewData["SupplierID"] = new SelectList(await _getSuppliersAsyncUseCase.Execute(), "ID", "SupplierName");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SupplierID,ProductNumber,Description,Picture")] Product product, List<IFormFile> theFiles)
        {   

            if (ModelState.IsValid)
            {
                await AddDocumentsAsync(product, theFiles);
                await _addProductAsyncUseCase.Execute(product);
                return RedirectToAction(nameof(Index));
            }
            LoadSelectList(product);
            return View(product);

        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _getProductByIDAsyncUseCase.Execute(id);
            if (product == null)
            {
                return NotFound();
            }
            LoadSelectList(product);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SupplierID,ProductNumber,Description")] Product product, List<IFormFile> theFiles)
        {
            product.ProductPictures = _getProductByIDAsyncUseCase.Execute(id).Result.ProductPictures;

            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await AddDocumentsAsync(product, theFiles);
                    await _updateProductAsyncUseCase.Execute(id, product);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dex)
                {
                    string message = dex.GetBaseException().Message;
                    if (message.Contains("UNIQUE"))
                    {
                        ModelState.AddModelError("", "Unique constraint failed");
                    }
                    else
                    {
                        ModelState.AddModelError("", dex.GetBaseException().Message);
                    }
                }
                
            }
            LoadSelectList(product);
            return View(product);
            
            
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _getProductByIDAsyncUseCase.Execute(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _getProductByIDAsyncUseCase.Execute(id);
            if (supplier != null)
            {
                await _delProductAsyncUseCase.Execute(supplier.ID);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task AddDocumentsAsync(Product product, List<IFormFile> theFiles)
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
                        ProductPicture p = new ProductPicture();
                        using (var memoryStream = new MemoryStream())
                        {
                            await f.CopyToAsync(memoryStream);

                            p.FileContent.Content = memoryStream.ToArray();

                        }
                        p.MimeType = mimeType;
                        p.FileName = fileName;
                        product.ProductPictures.Add(p);
                    };
                }
            }
        }

        private bool ProductExists(int id)
        {
            var supplier = _getProductByIDAsyncUseCase.Execute(id);

            return supplier != null;
        }

        private async void LoadSelectList(Product product)
        {
            ViewData["SupplierID"] = new SelectList(await _getSuppliersAsyncUseCase.Execute(), "ID", "SupplierName", product.SupplierID);
        }

    }
}
