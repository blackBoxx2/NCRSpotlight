using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntitiesLayer.Models;
using NCRSPOTLIGHT.Data;
using Plugins.DataStore.SQLite;
using UseCasesLayer.UseCaseInterfaces.SuppliersUseCaseInterfaces;

namespace NCRSPOTLIGHT.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IAddSupplierAsyncUseCase _addSupplierAsyncUseCase;
        private readonly IDeleteSupplierAsyncUseCase _deleteSupplierAsyncUseCase;
        private readonly IGetSupplierByIDAsyncUseCase _getSupplierByIDAsyncUseCase;
        private readonly IGetSuppliersAsyncUseCase _getSuppliersAsyncUseCase;
        private readonly IUpdateSupplierAsycUseCase _updateSupplierAsycUseCase;

        public SupplierController(IAddSupplierAsyncUseCase addSupplierAsyncUseCase,
                                  IDeleteSupplierAsyncUseCase deleteSupplierAsyncUseCase,
                                  IGetSupplierByIDAsyncUseCase getSupplierByIDAsyncUseCase,
                                  IGetSuppliersAsyncUseCase getSuppliersAsyncUseCase,
                                  IUpdateSupplierAsycUseCase updateSupplierAsycUseCase)
        {
            this._addSupplierAsyncUseCase = addSupplierAsyncUseCase;
            this._deleteSupplierAsyncUseCase = deleteSupplierAsyncUseCase;
            this._getSupplierByIDAsyncUseCase = getSupplierByIDAsyncUseCase;
            this._getSuppliersAsyncUseCase = getSuppliersAsyncUseCase;
            this._updateSupplierAsycUseCase = updateSupplierAsycUseCase;
        }

        // GET: Supplier
        public async Task<IActionResult> Index()
        {
            var suppliers = await _getSuppliersAsyncUseCase.Execute();
            return View(suppliers);
        }

        // GET: Supplier/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _getSupplierByIDAsyncUseCase.Execute(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Supplier/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SupplierName")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                await _addSupplierAsyncUseCase.Execute(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Supplier/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _getSupplierByIDAsyncUseCase.Execute(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SupplierName")] Supplier supplier)
        {
            if (id != supplier.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _updateSupplierAsycUseCase.Execute(id,supplier);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await SupplierExists(supplier.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Supplier/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _getSupplierByIDAsyncUseCase.Execute(id);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // POST: Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplier = await _getSupplierByIDAsyncUseCase.Execute(id);
            if (supplier != null)
            {
                await _deleteSupplierAsyncUseCase.Execute(supplier.ID);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> SupplierExists(int id)
        {
            var supplier = await _getSupplierByIDAsyncUseCase.Execute(id);

            return supplier != null; 
        }
    }
}
