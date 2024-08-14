using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.MVC.Models;
using Project.Service.Models;
using Project.Service.GetParameters;
using Project.Service.Services;

namespace Project.MVC.Controllers
{
    public class MakeController : Controller
    {
        private readonly IMakeService _makeService;
        private readonly IMapper _mapper;

        public MakeController(IMakeService makeService, IMapper mapper)
        {
            _makeService = makeService;
            _mapper = mapper;
        }

       
        public async Task<IActionResult> Index(MakeGetParameters makeGetParameters)
        {
           

            ViewData["CurrentSort"] = makeGetParameters.SortOrder;
            ViewData["NameSortParam"] = makeGetParameters.SortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewData["AbrvSortParam"] = makeGetParameters.SortOrder == "abrv_asc" ? "abrv_desc" : "abrv_asc";

            var makes = await _makeService.GetAllMakesAsync(makeGetParameters);

            var makeViewModels = makes.Select(m => _mapper.Map<VehicleMakeViewModel>(m)).ToList();
            return View(new PaginatedList<VehicleMakeViewModel>(makeViewModels, makes.Count, makes.PageIndex, makes.TotalPages));
        }

        
       

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Abrv")] VehicleMakeViewModel makeViewModel)
        {
            if (ModelState.IsValid)
            {
                var make = _mapper.Map<VehicleMake>(makeViewModel);
                await _makeService.InsertMakeAsync(make);
                return RedirectToAction(nameof(Index));
            }
            return View(makeViewModel);
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            var make = await _makeService.GetMakeByIdAsync(id);
            if (make == null)
            {
                return NotFound();
            }
            var makeViewModel = _mapper.Map<VehicleMakeViewModel>(make);
            return View(makeViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv")] VehicleMakeViewModel makeViewModel)
        {
            if (id != makeViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    var existingMake = await _makeService.GetMakeByIdAsync(id);

                    if (existingMake == null)
                    {
                        return NotFound(); 
                    }

                    _mapper.Map(makeViewModel, existingMake);

                    await _makeService.UpdateMakeAsync(existingMake);

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                    ModelState.AddModelError(string.Empty, "The record you attempted to edit was modified by another user after you got the original value. The edit operation was canceled. If you still want to edit this record, please reload the page and try again.");

                    var make = await _makeService.GetMakeByIdAsync(id);
                    var updatedMakeViewModel = _mapper.Map<VehicleMakeViewModel>(make);

                    return View(updatedMakeViewModel);
                }
            }

            return View(makeViewModel);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _makeService.DeleteMakeAsync(id);
            return RedirectToAction(nameof(Index));
        }




    }
}
