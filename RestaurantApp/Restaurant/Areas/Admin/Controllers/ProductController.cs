using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller{
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }
        public IActionResult Get(int id){
            var model = _manager.ProductService.GetOne(id, false);
            return View(model);
        }
        public async Task<IActionResult> Index(){
            var productTask = _manager.ProductService.GetAll(false);
            var products = await productTask;

            var model = products.ToList();
            return View(model);
        }
        public IActionResult Create(){
            ViewBag.Categories = GetCategoriesSelectList();
            return View();
        }
        private async Task<SelectList> GetCategoriesSelectList(){
            return new SelectList(await _manager.CategoryService.GetAll(false), "CategoryId", "CategoryName", 1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto, IFormFile file){
            if(ModelState.IsValid){
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl = String.Concat("/images/", file.FileName);
                _manager.ProductService.CreateOne(productDto);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Update([FromRoute]int id){
            ViewBag.Categories = GetCategoriesSelectList();
            var model = _manager.ProductService.GetOneForUpdate(id, true);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm]ProductDtoForUpdate productDto, IFormFile file){
            if(ModelState.IsValid){
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName); //Proje sunucuda nerede yer alıyorsa o adrese git, sonrasında wwwroot/images/file.filename adresini kullan.
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                productDto.ImageUrl = String.Concat("/images/", file.FileName);
                _manager.ProductService.UpdateOne(productDto);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete([FromRoute(Name = "id")]int id){
            _manager.ProductService.DeleteOne(id);
            return RedirectToAction("Index");
        }
    }   
}