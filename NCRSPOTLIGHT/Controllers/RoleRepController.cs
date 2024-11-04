using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntitiesLayer.Models;
using Plugins.DataStore.SQLite;
using UseCasesLayer.UseCaseInterfaces.SuppliersUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.RoleRepUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.SuppliersUseCases;

namespace NCRSPOTLIGHT.Controllers
{
    public class RoleRepController : Controller
    {
        private readonly IAddRoleRepAsyncUseCase _addRoleRepAsyncUseCase;
        private readonly IDeleteRoleRepAsyncUseCase _deleteRoleRepAsyncUseCase;
        private readonly IGetRoleRepByIDAsyncUseCase _getRoleRepByIDAsyncUseCase;
        private readonly IGetRoleRepAsyncUseCase _getRoleRepAsyncUseCase;
        private readonly IUpdateRoleRepAsyncUseCase _updateRoleRepAsyncUseCase;

        // GET: RoleRep
        public async Task<IActionResult> Index()
        {
            var roleRep = await _getRoleRepAsyncUseCase.Execute();
            return View(roleRep);
        }

        // GET: RoleRep/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleRep = await _getRoleRepByIDAsyncUseCase.Execute(id);
            if (roleRep == null)
            {
                return NotFound();
            }

            return View(roleRep);
        }

        // GET: RoleRep/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleRep/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleRepID,RoleID,RepresentativeID")] RoleRep roleRep)
        {
            if (ModelState.IsValid)
            {
                await _addRoleRepAsyncUseCase.Execute(roleRep);
                return RedirectToAction(nameof(Index));
            }
            return View(roleRep);
        }

        // GET: RoleRep/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleRep = await _getRoleRepByIDAsyncUseCase.Execute(id);
            if (roleRep == null)
            {
                return NotFound();
            }
            return View(roleRep);
        }

        // POST: RoleRep/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleRepID,RoleID,RepresentativeID")] RoleRep roleRep)
        {
            if (id != roleRep.RoleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _updateRoleRepAsyncUseCase.Execute(id, roleRep);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RoleRepExists(roleRep.RoleID))
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
            return View(roleRep);
        }

            // GET: RoleRep/Delete/5
            public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleRep = await _getRoleRepByIDAsyncUseCase.Execute(id);
            if (roleRep == null)
            {
                return NotFound();
            }

            return View(roleRep);
        }

        // POST: RoleRep/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roleRep = await _getRoleRepByIDAsyncUseCase.Execute(id);
            if (roleRep != null)
            {
                await _deleteRoleRepAsyncUseCase.Execute(roleRep.RoleID);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RoleRepExists(int id)
        {
            var roleRep = await _getRoleRepByIDAsyncUseCase.Execute(id);

            return roleRep != null;
        }
    }
}
