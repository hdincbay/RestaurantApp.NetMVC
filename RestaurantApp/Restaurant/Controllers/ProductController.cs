using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

namespace Restaurant.Controllers
{
    public class ProductController : Controller{
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(){
            var model = _manager.ProductService.GetAll(false);
            return View(model);
        }
    }
}