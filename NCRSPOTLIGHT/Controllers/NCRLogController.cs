using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntitiesLayer.Models;
using Plugins.DataStore.SQLite;
using UseCasesLayer.UseCaseInterfaces.NCRLogUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.QualityPortionUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.QualityPortionUseCase;
using Microsoft.AspNetCore.Authorization;

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

        public NCRLogController(IAddNCRLogAsyncUseCase addNCRLogAsyncUseCase,
                                IDeleteNCRLogAsyncUseCase deleteNCRLogAsyncUseCase,
                                IGetNCRLogByIDAsyncUseCase getNCRLogByIDAsyncUseCase,
                                IGetNCRLogsAsyncUseCase getNCRLogsAsyncUseCase,
                                IUpdateNCRLogAsyncUseCase updateNCRLogAsyncUseCase,
                                IGetQualityPortionsAsyncUseCase getQualityPortionsAsyncUseCase
                                )
        {
            _addNCRLogAsyncUseCase = addNCRLogAsyncUseCase;
            _delNCRLogAsyncUseCase = deleteNCRLogAsyncUseCase;
            _getNCRLogByIDAsyncUseCase = getNCRLogByIDAsyncUseCase;
            _getNCRLogsAsyncUseCase = getNCRLogsAsyncUseCase;
            _updateNCRLogAsyncUseCase = updateNCRLogAsyncUseCase;
            _getQualityPortionsAsyncUseCase = getQualityPortionsAsyncUseCase;
        }

        // GET: NCRLog
        public async Task<IActionResult> Index()
        {
            var nCRContext = await _getNCRLogsAsyncUseCase.Execute();
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
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewData["QualityPortionID"] = new SelectList(await _getQualityPortionsAsyncUseCase.Execute(), "ID", "DefectDescription");
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
                await _addNCRLogAsyncUseCase.Execute(nCRLog);
                
                return RedirectToAction(nameof(Index));
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
            ViewData["QualityPortionID"] = new SelectList(await _getQualityPortionsAsyncUseCase.Execute(), "ID", "DefectDescription", nCRLog.QualityPortionID);
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
                    await _updateNCRLogAsyncUseCase.Execute(id, nCRLog);                    
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
            ViewData["QualityPortionID"] = new SelectList(await _getQualityPortionsAsyncUseCase.Execute(), "ID", "DefectDescription", nCRLog.QualityPortionID);
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

        private bool NCRLogExists(int id)
        {
            var log = _getNCRLogByIDAsyncUseCase.Execute(id);
            return log != null;
        }     
    }
}
