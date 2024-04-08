using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Restaurant.Components
{
    public class CategoriesMenuViewComponent : ViewComponent{
        private readonly IServiceManager _manager;

        public CategoriesMenuViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IViewComponentResult> InvokeAsync(){
            var model = await _manager.CategoryService.GetAll(false);
            return View(model);
        }
    }
}