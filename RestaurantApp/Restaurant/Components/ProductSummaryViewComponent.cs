using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Restaurant.Components
{
    public class ProductSummary : ViewComponent{
        private readonly IServiceManager _manager;

        public ProductSummary(IServiceManager manager)
        {
            _manager = manager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _manager.ProductService.GetAll(false);
            var count = products.Count().ToString();
            return Content(count);
        }
    }
}