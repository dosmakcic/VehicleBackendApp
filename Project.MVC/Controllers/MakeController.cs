using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.Service.Services;
using Project.MVC.Models;
using System.Dynamic;
using Project.Service.Models;

namespace Project.MVC.Controllers
{
    public class MakeController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public MakeController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

       
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber)
        {
            var makes = await _vehicleService.GetAllMakesAsync(sortOrder, searchString, pageNumber, 10);
            var makeViewModels = makes.Select(m => _mapper.Map<VehicleMakeViewModel>(m)).ToList();
            return View(new PaginatedList<VehicleMakeViewModel>(makeViewModels, makes.Count, makes.PageIndex, makes.TotalPages));
        }

        
        public async Task<IActionResult> Details(int id)
        {
            var make = await _vehicleService.GetMakeByIdAsync(id);
            if (make == null)
            {
                return NotFound();
            }
            var makeViewModel = _mapper.Map<VehicleMakeViewModel>(make);
            return View(makeViewModel);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMakeViewModel makeViewModel)
        {
            if (ModelState.IsValid)
            {
                var make = _mapper.Map<VehicleMake>(makeViewModel);
                await _vehicleService.InsertMakeAsync(make);
                return RedirectToAction(nameof(Index));
            }
            return View(makeViewModel);
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            var make = await _vehicleService.GetMakeByIdAsync(id);
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
                var make = _mapper.Map<VehicleMake>(makeViewModel);
                await _vehicleService.UpdateMakeAsync(make);
                return RedirectToAction(nameof(Index));
            }
            return View(makeViewModel);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var make = await _vehicleService.GetMakeByIdAsync(id);
            if (make == null)
            {
                return NotFound();
            }
            var makeViewModel = _mapper.Map<VehicleMakeViewModel>(make);
            return View(makeViewModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleService.DeleteMakeAsync(id);
            return RedirectToAction(nameof(Index));
        }




    }
}
