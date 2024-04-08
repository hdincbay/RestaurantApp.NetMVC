using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Restaurant.Components
{
    public class ShowcaseViewComponent : ViewComponent
    {
        private readonly IServiceManager _manager;

        public ShowcaseViewComponent(IServiceManager manager)
        {
            _manager = manager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _manager.ProductService.GetShowcaseProducts(false);
            return View(products);
        }
    }
}