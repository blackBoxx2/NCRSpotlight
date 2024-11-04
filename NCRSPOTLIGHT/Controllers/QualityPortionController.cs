using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntitiesLayer.Models;
using Plugins.DataStore.SQLite;

namespace NCRSPOTLIGHT.Controllers
{
    public class QualityPortionController : Controller
    {
        private readonly NCRContext _context;

        public QualityPortionController(NCRContext context)
        {
            _context = context;
        }

        // GET: QualityPortion
        public async Task<IActionResult> Index()
        {
            var nCRContext = _context.QualityPortions.Include(q => q.Product).Include(q => q.RoleRep);
            return View(await nCRContext.ToListAsync());
        }

        // GET: QualityPortion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualityPortion = await _context.QualityPortions
                .Include(q => q.Product)
                .Include(q => q.RoleRep)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (qualityPortion == null)
            {
                return NotFound();
            }

            return View(qualityPortion);
        }

        // GET: QualityPortion/Create
        public IActionResult Create()
        {
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Description");
            ViewData["RoleRepID"] = new SelectList(_context.RoleReps, "RoleRepID", "RoleRepID");
            return View();
        }

        // POST: QualityPortion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProductID,Quantity,QuantityDefective,OrderNumber,DefectDescription,RoleRepID")] QualityPortion qualityPortion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(qualityPortion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Description", qualityPortion.ProductID);
            ViewData["RoleRepID"] = new SelectList(_context.RoleReps, "RoleRepID", "RoleRepID", qualityPortion.RoleRepID);
            return View(qualityPortion);
        }

        // GET: QualityPortion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualityPortion = await _context.QualityPortions.FindAsync(id);
            if (qualityPortion == null)
            {
                return NotFound();
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Description", qualityPortion.ProductID);
            ViewData["RoleRepID"] = new SelectList(_context.RoleReps, "RoleRepID", "RoleRepID", qualityPortion.RoleRepID);
            return View(qualityPortion);
        }

        // POST: QualityPortion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductID,Quantity,QuantityDefective,OrderNumber,DefectDescription,RoleRepID")] QualityPortion qualityPortion)
        {
            if (id != qualityPortion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(qualityPortion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QualityPortionExists(qualityPortion.ID))
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
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "Description", qualityPortion.ProductID);
            ViewData["RoleRepID"] = new SelectList(_context.RoleReps, "RoleRepID", "RoleRepID", qualityPortion.RoleRepID);
            return View(qualityPortion);
        }

        // GET: QualityPortion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var qualityPortion = await _context.QualityPortions
                .Include(q => q.Product)
                .Include(q => q.RoleRep)
                .FirstOrDefaultAsync(m => m.ID == id);
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
            var qualityPortion = await _context.QualityPortions.FindAsync(id);
            if (qualityPortion != null)
            {
                _context.QualityPortions.Remove(qualityPortion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QualityPortionExists(int id)
        {
            return _context.QualityPortions.Any(e => e.ID == id);
        }
    }
}
