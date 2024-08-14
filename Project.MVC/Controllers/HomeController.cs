using Microsoft.AspNetCore.Mvc;
using Project.MVC.Models;
using Project.Service.Services;

namespace Project.MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly IModelService _vehicleService;

        public HomeController(IModelService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            var requestId = HttpContext.TraceIdentifier;
            return View(new ErrorViewModel { RequestId = requestId });
        }
    }
}
