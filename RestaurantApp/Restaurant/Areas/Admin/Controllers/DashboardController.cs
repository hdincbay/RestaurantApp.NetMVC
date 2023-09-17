using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller{
        private readonly Cart _cart;

        public DashboardController(Cart cart)
        {
            _cart = cart;
        }

        public IActionResult Index(){
            return View();
        }
        public IActionResult Orders()
        {
            var model = _cart.Lines.ToArray();
            return View(model);
        }
    }
}