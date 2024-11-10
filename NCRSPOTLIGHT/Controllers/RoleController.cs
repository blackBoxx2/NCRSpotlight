using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntitiesLayer.Models;
using Plugins.DataStore.SQLite;
using UseCasesLayer.UseCaseInterfaces.RoleUseCaseInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace NCRSPOTLIGHT.Controllers
{
    public class RoleController : Controller
    {
        private readonly IAddRoleAsyncUserCase _addRoleAsyncUserCase;
        private readonly IDeleteRoleAsyncUserCase _deleteRoleAsyncUserCase;
        private readonly IGetRoleByIDAsyncUserCase _getRoleByIDAsyncUserCase;
        private readonly IGetRoleAsyncUserCase _getRoleAsyncUserCase;
        private readonly IUpdateRoleAsyncUserCase _updateRoleAsyncUserCase;

        public RoleController(IAddRoleAsyncUserCase addRoleAsyncUserCase,
                              IDeleteRoleAsyncUserCase deleteRoleAsyncUserCase,
                              IGetRoleByIDAsyncUserCase getRoleByIDAsyncUserCase,
                              IGetRoleAsyncUserCase getRoleAsyncUserCase,
                              IUpdateRoleAsyncUserCase updateRoleAsyncUserCase)
        {
            this._addRoleAsyncUserCase = addRoleAsyncUserCase;
            this._deleteRoleAsyncUserCase = deleteRoleAsyncUserCase;
            this._getRoleByIDAsyncUserCase = getRoleByIDAsyncUserCase;
            this._getRoleAsyncUserCase = getRoleAsyncUserCase;
            this._updateRoleAsyncUserCase = updateRoleAsyncUserCase;
        }



        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var role = await _getRoleAsyncUserCase.Execute();
            return View(role);
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _getRoleByIDAsyncUserCase.Execute(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Roles/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                
                await _addRoleAsyncUserCase.Execute(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _getRoleByIDAsyncUserCase.Execute(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,RoleName")] IdentityRole role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    await _updateRoleAsyncUserCase.Execute(id, role);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RoleExists(role.Id))
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
            return View(role);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _getRoleByIDAsyncUserCase.Execute(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _getRoleByIDAsyncUserCase.Execute(id);
            if (role != null)
            {
                await _deleteRoleAsyncUserCase.Execute(role.Id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RoleExists(string id)
        {
            var role = await _getRoleByIDAsyncUserCase.Execute(id);
            return role != null;
        }
    }
}
