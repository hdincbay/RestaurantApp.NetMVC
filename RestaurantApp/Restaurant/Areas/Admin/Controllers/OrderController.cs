using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IServiceManager _manager;

        public OrderController(IServiceManager manager)
        {
            _manager = manager;
        }
        public IActionResult Index()
        {
            var model = _manager.OrderService.Orders.ToList();
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Complete([FromForm]int id)
        {
            _manager.OrderService.Complete(id);
            return RedirectToAction("Index");
        }
    }
}