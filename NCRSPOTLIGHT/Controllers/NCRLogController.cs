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
    public class NCRLogController : Controller
    {
        private readonly NCRContext _context;

        public NCRLogController(NCRContext context)
        {
            _context = context;
        }

        // GET: NCRLog
        public async Task<IActionResult> Index()
        {
            var nCRContext = _context.NCRLog.Include(n => n.QualityPortion);
            return View(await nCRContext.ToListAsync());
        }

        // GET: NCRLog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nCRLog = await _context.NCRLog
                .Include(n => n.QualityPortion)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nCRLog == null)
            {
                return NotFound();
            }

            return View(nCRLog);
        }

        // GET: NCRLog/Create
        public IActionResult Create()
        {
            ViewData["QualityPortionID"] = new SelectList(_context.QualityPortions, "ID", "DefectDescription");
            return View();
        }

        // POST: NCRLog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,QualityPortionID,DateCreated,Status")] NCRLog nCRLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nCRLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QualityPortionID"] = new SelectList(_context.QualityPortions, "ID", "DefectDescription", nCRLog.QualityPortionID);
            return View(nCRLog);
        }

        // GET: NCRLog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nCRLog = await _context.NCRLog.FindAsync(id);
            if (nCRLog == null)
            {
                return NotFound();
            }
            ViewData["QualityPortionID"] = new SelectList(_context.QualityPortions, "ID", "DefectDescription", nCRLog.QualityPortionID);
            return View(nCRLog);
        }

        // POST: NCRLog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,QualityPortionID,DateCreated,Status")] NCRLog nCRLog)
        {
            if (id != nCRLog.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nCRLog);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["QualityPortionID"] = new SelectList(_context.QualityPortions, "ID", "DefectDescription", nCRLog.QualityPortionID);
            return View(nCRLog);
        }

        // GET: NCRLog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nCRLog = await _context.NCRLog
                .Include(n => n.QualityPortion)
                .FirstOrDefaultAsync(m => m.ID == id);
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
            var nCRLog = await _context.NCRLog.FindAsync(id);
            if (nCRLog != null)
            {
                _context.NCRLog.Remove(nCRLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NCRLogExists(int id)
        {
            return _context.NCRLog.Any(e => e.ID == id);
        }
    }
}
