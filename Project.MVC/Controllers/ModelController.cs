using Microsoft.AspNetCore.Mvc;
using Project.Service.Services;
using AutoMapper;
using Project.MVC.Models;
using Project.Service.Models;

namespace Project.MVC.Controllers
{
    public class ModelController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;


        public ModelController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

      
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber)
        {
            var models = await _vehicleService.GetAllModelsAsync(sortOrder, searchString, pageNumber, 10);
            var modelViewModels = models.Select(m => _mapper.Map<VehicleModelViewModel>(m)).ToList();
            return View(new PaginatedList<VehicleModelViewModel>(modelViewModels, models.Count, models.PageIndex, models.TotalPages));
        }

       
        public async Task<IActionResult> Details(int id)
        {
            var model = await _vehicleService.GetModelByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var modelViewModel = _mapper.Map<VehicleModelViewModel>(model);
            return View(modelViewModel);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MakeId,Name,Abrv")] VehicleModelViewModel modelViewModel)
        {
            if (ModelState.IsValid)
            {
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
                var model = _mapper.Map<VehicleModel>(modelViewModel);
                await _vehicleService.UpdateModelAsync(model);
                return RedirectToAction(nameof(Index));
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
