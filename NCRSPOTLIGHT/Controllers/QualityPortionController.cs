using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EntitiesLayer.Models;
using Plugins.DataStore.SQLite;
using UseCasesLayer.UseCaseInterfaces.RepresentitiveUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.QualityPortionUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.QualityPortionUseCase;
using UseCasesLayer.UseCaseInterfaces.RoleRepUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.ProductUseCaseInterfaces;
using UseCasesLayer.UseCaseInterfaces.RoleUseCaseInterfaces;
using Plugins.DataStore.SQLite.JoinsUseCase;
using EntitiesLayer.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace NCRSPOTLIGHT.Controllers
{
    public class QualityPortionController : Controller
    {
        private readonly IAddQualityPortionAsyncUseCase _addQualityPortionAsyncUseCase;
        private readonly IDeleteQualityPortionAsyncUseCase _deleteQualityPortionAsyncUseCase;
        private readonly IGetQualityPortionsAsyncUseCase _getQualityPortionsAsyncUseCase;
        private readonly IGetQualityPortionByIDAsyncUseCase _getQualityPortionByIDAsyncUseCase;
        private readonly IUpdateQualityPortionAsyncUseCase _updateQualityPortionAsyncUseCase;
        private readonly IGetRoleRepAsyncUseCase _getRoleRepAsyncUseCase;
        private readonly IGetProductsAsyncUseCase _getProductsAsyncUseCase;
        private readonly IGetRoleAsyncUserCase getRoleAsyncUserCase;
        private readonly QualityPortionService qualityPortionService;
        private readonly IdentityContext identityContext;

        public QualityPortionController(IAddQualityPortionAsyncUseCase addQualityPortionAsyncUseCase,
                                        IDeleteQualityPortionAsyncUseCase deleteQualityPortionAsyncUseCase,
                                        IGetQualityPortionsAsyncUseCase getQualityPortionsAsyncUseCase,
                                        IGetQualityPortionByIDAsyncUseCase getQualityPortionByIDAsyncUseCase,
                                        IUpdateQualityPortionAsyncUseCase updateQualityPortionAsyncUseCase,
                                        IGetRoleRepAsyncUseCase getRoleRepAsyncUseCase,
                                        IGetProductsAsyncUseCase getProductsAsyncUseCase,
                                        IGetRoleAsyncUserCase getRoleAsyncUserCase,
                                        QualityPortionService qualityPortionService,
                                        IdentityContext identityContext)
        {
            _addQualityPortionAsyncUseCase = addQualityPortionAsyncUseCase;
            _deleteQualityPortionAsyncUseCase = deleteQualityPortionAsyncUseCase;
            _getQualityPortionByIDAsyncUseCase = getQualityPortionByIDAsyncUseCase;
            _getQualityPortionsAsyncUseCase = getQualityPortionsAsyncUseCase;
            _updateQualityPortionAsyncUseCase = updateQualityPortionAsyncUseCase;
            _getRoleRepAsyncUseCase = getRoleRepAsyncUseCase;  
            _getProductsAsyncUseCase = getProductsAsyncUseCase;
            this.getRoleAsyncUserCase = getRoleAsyncUserCase;
            this.qualityPortionService = qualityPortionService;
            this.identityContext = identityContext;
        }

        // GET: QualityPortion
        public async Task<IActionResult> Index()
        {
            var qualityPortions = await qualityPortionService.GetQualityPortionsWithRepresentativeAsync();
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
            var qualityPortionviewModel = new QualityPortionsWithRepViewModel();

            qualityPortionviewModel.QualityPortion = new QualityPortion();

            LoadSelectList(qualityPortionviewModel);
            return View(qualityPortionviewModel);
        }

        // POST: QualityPortion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QualityPortionsWithRepViewModel qualityPortionViewModel, List<IFormFile> theFiles)
        {
            qualityPortionViewModel.Representative = await identityContext.Users.FirstOrDefaultAsync( u => u.Id == qualityPortionViewModel.QualityPortion.RepId);

            if (ModelState.IsValid)
            {
                await AddDocumentsAsync(qualityPortionViewModel.QualityPortion, theFiles);
                await _addQualityPortionAsyncUseCase.Execute(qualityPortionViewModel.QualityPortion);            
                return RedirectToAction(nameof(Index));
            }
            LoadSelectList(qualityPortionViewModel);
            return View(qualityPortionViewModel);
        }

        // GET: QualityPortion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var qualityPortionViewModel= new QualityPortionsWithRepViewModel();
            qualityPortionViewModel.QualityPortion = await _getQualityPortionByIDAsyncUseCase.Execute(id);

            if (qualityPortionViewModel.QualityPortion == null)
            {
                return NotFound();
            }
            LoadSelectList(qualityPortionViewModel);
            return View(qualityPortionViewModel);
        }

        // POST: QualityPortion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ProductID,Quantity,QuantityDefective,OrderNumber,DefectDescription,RoleRepID")] QualityPortionsWithRepViewModel qualityPortionViewModel, List<IFormFile> theFiles)
        {

            qualityPortionViewModel.QualityPortion.qualityPictures = _getQualityPortionByIDAsyncUseCase.Execute(id).Result.qualityPictures;

            if (id != qualityPortionViewModel.QualityPortion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await AddDocumentsAsync(qualityPortionViewModel.QualityPortion, theFiles);
                    await _updateQualityPortionAsyncUseCase.Execute(id, qualityPortionViewModel.QualityPortion);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await QualityPortionExists(qualityPortionViewModel.QualityPortion.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            LoadSelectList(qualityPortionViewModel);
            return View(qualityPortionViewModel);
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
        public async void LoadSelectList(QualityPortionsWithRepViewModel qualityPortionviewModel)
        {
            qualityPortionviewModel.QualityPortion = new QualityPortion();

            var roles = await getRoleAsyncUserCase.Execute();
            var qa = identityContext.Roles
                .FirstOrDefault(r => r.Name == "QualityAssurance").Id;
            if (qa != null)
            {
                var userRoleIds = await identityContext.UserRoles
                .Where(ur => ur.RoleId == qa)
                .Select(ur => ur.UserId)
                .ToListAsync();

                var selectListItems = await identityContext.Users
                    .Where(u => userRoleIds.Contains(u.Id))
                    .ToListAsync();
                ViewBag.RepId = new SelectList(selectListItems, "Id", "UserName", qualityPortionviewModel.QualityPortion.RepId);

            }

            ViewBag.ProductID = new SelectList(await _getProductsAsyncUseCase.Execute(), "ID", "Description", qualityPortionviewModel.QualityPortion.ProductID);
            if (ViewBag.RepID == null)
            {
                throw new Exception("Rep List is Empty");
            }
            else if(ViewBag.ProductId == null)
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
