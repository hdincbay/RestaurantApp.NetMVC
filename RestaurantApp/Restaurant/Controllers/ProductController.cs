using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace Restaurant.Controllers
{
    public class ProductController : Controller{

        private readonly IServiceManager _manager;        
        private readonly ILogger<ProductController> _logger;
        public ProductController(IServiceManager manager, ILogger<ProductController> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task<IActionResult> Index(){
            _logger.LogInformation("Test log");
            var model = await Task.Run(() => _manager.ProductService.GetAll(false));
            return View(model);
        }
        public async Task<IActionResult> Get(int id){
            var model = await Task.Run(() => _manager.ProductService.GetOne(id, true));
            return View(model);
        }
    }
}