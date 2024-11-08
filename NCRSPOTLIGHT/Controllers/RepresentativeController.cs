﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntitiesLayer.Models;
using Plugins.DataStore.SQLite;
using UseCasesLayer.UseCaseInterfaces.RepresentitiveUseCaseInterfaces;
using SQLitePCL;

namespace NCRSPOTLIGHT.Controllers
{
    public class RepresentativeController : Controller
    {
        private readonly IAddRepresentativeAsyncUseCase _addRepresentativeAsyncUseCase;
        private readonly IDeleteRepresentativeAsyncUseCase _deleteRepresentativeAsyncUseCase;
        private readonly IGetRepresentativesAsyncUseCase _getRepresentativesAsyncUseCase;
        private readonly IGetRepresentativesByIdAsyncUseCase _getRepresentativesByIdAsyncUseCase;
        private readonly IUpdateRepresentativeAsyncUseCase _updateRepresentativeAsyncUseCase;

        public RepresentativeController(IAddRepresentativeAsyncUseCase addRepresentativeAsyncUseCase,
            IDeleteRepresentativeAsyncUseCase deleteRepresentativeAsyncUseCase,
            IGetRepresentativesAsyncUseCase getRepresentativesAsyncUseCase,
            IGetRepresentativesByIdAsyncUseCase getRepresentativesByIdAsyncUseCase,
            IUpdateRepresentativeAsyncUseCase updateRepresentativeAsyncUseCase)
        {
            this._addRepresentativeAsyncUseCase = addRepresentativeAsyncUseCase;
            this._getRepresentativesAsyncUseCase = getRepresentativesAsyncUseCase;
            this._getRepresentativesByIdAsyncUseCase = getRepresentativesByIdAsyncUseCase;
            this._deleteRepresentativeAsyncUseCase = deleteRepresentativeAsyncUseCase;
            this._updateRepresentativeAsyncUseCase = updateRepresentativeAsyncUseCase;
        }

        // GET: Representative
        public async Task<IActionResult> Index()
        {
            var representatives = await _getRepresentativesAsyncUseCase.Execute();
            return View(representatives);
        }

        // GET: Representative/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representative = await _getRepresentativesByIdAsyncUseCase.Execute(id);
            if (representative == null)
            {
                return NotFound();
            }

            return View(representative);
        }

        // GET: Representative/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Representative/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,MiddleInitial,LastName")] Representative representative)
        {
            if (ModelState.IsValid)
            {
                await _addRepresentativeAsyncUseCase.Execute(representative);
                return RedirectToAction(nameof(Index));
            }
            return View(representative);
        }

        // GET: Representative/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representative = await _getRepresentativesByIdAsyncUseCase.Execute(id);
            if (representative == null)
            {
                return NotFound();
            }
            return View(representative);
        }

        // POST: Representative/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,MiddleInitial,LastName")] Representative representative)
        {
            if (id != representative.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _updateRepresentativeAsyncUseCase.Execute(id, representative);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await RepresentativeExists(representative.ID))
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
            return View(representative);
        }

        // GET: Representative/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representative = await _getRepresentativesByIdAsyncUseCase.Execute(id);
            if (representative == null)
            {
                return NotFound();
            }

            return View(representative);
        }

        // POST: Representative/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var representative = await _getRepresentativesByIdAsyncUseCase.Execute(id);
            if (representative != null)
            {
                await _deleteRepresentativeAsyncUseCase.Execute(representative.ID);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RepresentativeExists(int id)
        {
            var representative = await _getRepresentativesByIdAsyncUseCase.Execute(id);
            return representative != null;
        }
    }
}
