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
            _logger.LogInformation("ProductController: Index (+)");
            _logger.LogInformation("ProductController: Liste getiriliyor...");
            var model = await Task.Run(() => _manager.ProductService.GetAll(false));
            var modelCount = model.Count();
            _logger.LogInformation($"ProductController: Toplam {model.Count()} adet �r�n getirildi.");
            return View(model);
        }
        public async Task<IActionResult> Get(int id){
            _logger.LogInformation("ProductController: Index (+)");
            _logger.LogInformation($"ProductController: {id} numaral� �r�n getiriliyor...");
            var model = await Task.Run(() => _manager.ProductService.GetOne(id, true));
            _logger.LogInformation($"ProductController: {model.ProductName} isimli �r�n getirildi.");
            return View(model);
        }
    }
}