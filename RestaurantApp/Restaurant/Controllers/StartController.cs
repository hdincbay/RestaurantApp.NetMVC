using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Restaurant.Controllers
{
    public class StartController : Controller
    {
        private readonly IServiceManager _manager;

        public StartController(IServiceManager manager)
        {
            _manager = manager;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Menu()
        {
            var model = _manager.ProductService.GetAll(false);
            return View(model);
        }
    }
}
