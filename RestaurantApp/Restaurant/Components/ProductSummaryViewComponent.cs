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

        public string Invoke(){
            return _manager.ProductService.GetAll(false).Count().ToString();
        } 
    }
}