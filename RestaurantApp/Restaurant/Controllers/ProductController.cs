using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;

namespace Restaurant.Controllers
{
    public class ProductController : Controller{

        private readonly IServiceManager _manager;
        private readonly RepositoryContext _context;
        private readonly ILog log = LogManager.GetLogger(typeof(ProductController));

        public ProductController(IServiceManager manager, RepositoryContext context)
        {
            _manager = manager;
            _context = context;
        }

        public async Task<IActionResult> Index(){
            log.Info("ProductController: Index (+)");
            log.Info("ProductController: Liste getiriliyor...");
            var model = await _manager.ProductService.GetAll(false);
            var modelCount = model.Count();
            var categoryList = await _manager.CategoryService.GetAll(false);
            ViewBag.CategoryList = categoryList;
            log.Info($"ProductController: Toplam {model.Count()} adet ürün getirildi.");
            return View(model);
        }
        public async Task<IActionResult> Get(int id){
            log.Info("ProductController: Index (+)");
            log.Info($"ProductController: {id} numaralý ürün getiriliyor...");
            var model = await _manager.ProductService.GetOne(id, true);
            log.Info($"ProductController: {model.ProductName} isimli ürün getirildi.");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsByCategoryId([FromRoute] int id)
        {
            try
            {
                var productList = await _context.Products.Where(p => p.CategoryId.Equals(id)).ToListAsync();
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}