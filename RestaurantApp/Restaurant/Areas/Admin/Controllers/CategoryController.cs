using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller{
        private readonly IServiceManager _manager;

        public CategoryController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(){
            var model = _manager.CategoryService.GetAll(false);
            return View(model);
        }
    }
}