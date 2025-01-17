﻿using Microsoft.AspNetCore.Mvc;
using Project.Service.Services;
using AutoMapper;
using Project.MVC.Models;
using Project.Service.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Service.GetParameters;

namespace Project.MVC.Controllers
{
    public class ModelController : Controller
    {
        private readonly IModelService _vehicleService;
        private readonly IMakeService _makeService;
        private readonly IMapper _mapper;


        public ModelController(IModelService vehicleService,IMakeService makeService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _makeService = makeService;
            _mapper = mapper;
        }

      
        public async Task<IActionResult> Index(ModelGetParameters modelGetParameters)
        {


            
            var paginatedModels = await _vehicleService.GetAllModelsAsync(modelGetParameters);

           
            var uniqueMakeIds = paginatedModels
                .Select(m => m.MakeId)
                .Distinct()
                .ToList();


            var allMakes = await _makeService.TestGetMakes();
            var makes = allMakes
                       .Select(m => new SelectListItem
                       {
                           Value = m.Id.ToString(),
                           Text = m.Name,
                           Selected = m.Id == modelGetParameters.SelectedMakeId
                       }).ToList();

            ViewData["CurrentSort"] = modelGetParameters.SortOrder;
            ViewData["NameSortParam"] = modelGetParameters.SortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewData["AbrvSortParam"] = modelGetParameters.SortOrder == "abrv_asc" ? "abrv_desc" : "abrv_asc";
            ViewData["MakeIdSortParam"] = modelGetParameters.SortOrder == "makeId_asc" ? "makeId_desc" : "makeId_asc";



            ViewData["SelectedMakeId"] = modelGetParameters.SelectedMakeId;
            ViewData["Makes"] = makes;




            var modelViewModels = paginatedModels
                .Select(m => _mapper.Map<VehicleModelViewModel>(m))
                .ToList();

           

            return View(new PaginatedList<VehicleModelViewModel>(
                modelViewModels,
                paginatedModels.Count,
                paginatedModels.PageIndex,
                paginatedModels.TotalPages
            ));


        }

       
       

        
        public IActionResult Create()
        {
            return View();
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MakeId,Name,Abrv")] VehicleModelViewModel modelViewModel)
        {
            if (ModelState.IsValid)
            {
               
                var makeExists = await _makeService.GetMakeByIdAsync(modelViewModel.MakeId);
                if (makeExists == null)
                {
                    ModelState.AddModelError("MakeId", "Selected Make does not exist.");
                    return View(modelViewModel);
                }

                var model = _mapper.Map<VehicleModel>(modelViewModel);
                await _vehicleService.InsertModelAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(modelViewModel);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var model = await _vehicleService.GetModelByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var modelViewModel = _mapper.Map<VehicleModelViewModel>(model);
            return View(modelViewModel);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MakeId,Name,Abrv")] VehicleModelViewModel modelViewModel)
        {
            if (id != modelViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingModel = await _vehicleService.GetModelByIdAsync(id);
                    if (existingModel == null)
                    {
                        return NotFound();
                    }

                   
                    var makeExists = await _makeService.GetMakeByIdAsync(modelViewModel.MakeId);
                    if (makeExists == null)
                    {
                        ModelState.AddModelError("MakeId", "Selected Make does not exist.");
                        return View(modelViewModel);
                    }

                    _mapper.Map(modelViewModel, existingModel);
                    await _vehicleService.UpdateModelAsync(existingModel);

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "The record you attempted to edit was modified by another user after you got the original value. The edit operation was canceled. If you still want to edit this record, please reload the page and try again.");

                    var model = await _vehicleService.GetModelByIdAsync(id);
                    var updatedModelViewModel = _mapper.Map<VehicleModelViewModel>(model);
                    return View(updatedModelViewModel);
                }
            }
            return View(modelViewModel);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var model = await _vehicleService.GetModelByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var modelViewModel = _mapper.Map<VehicleModelViewModel>(model);
            return View(modelViewModel);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleService.DeleteModelAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
