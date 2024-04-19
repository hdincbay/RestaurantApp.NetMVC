using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace Restaurant.Controllers
{
    public class ProductController : Controller{

        private readonly IServiceManager _manager;
        private readonly ILog log = LogManager.GetLogger(typeof(ProductController));

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IActionResult> Index(){
            log.Info("ProductController: Index (+)");
            log.Info("ProductController: Liste getiriliyor...");
            var model = await Task.Run(() => _manager.ProductService.GetAll(false));
            var modelCount = model.Count();
            log.Info($"ProductController: Toplam {model.Count()} adet ürün getirildi.");
            return View(model);
        }
        public async Task<IActionResult> Get(int id){
            log.Info("ProductController: Index (+)");
            log.Info($"ProductController: {id} numaralý ürün getiriliyor...");
            var model = await Task.Run(() => _manager.ProductService.GetOne(id, true));
            log.Info($"ProductController: {model.ProductName} isimli ürün getirildi.");
            return View(model);
        }
    }
}