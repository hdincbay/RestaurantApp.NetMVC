using Entities.Dtos;
using Entities.Models;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;
using System;
using System.IO;
using System.Reflection;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller{
        private readonly IServiceManager _manager;
        private readonly ILog log = LogManager.GetLogger(typeof(ProductController));


        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }
        public IActionResult Get(int id){
            log.Debug($"{id} Numaralı Ürün getiriliyor...  {Assembly.GetAssembly(typeof(ProductController))} - Admin");
            var model = _manager.ProductService.GetOne(id, false);
            return View(model);
        }
        public async Task<IActionResult> Index(){
            try
            {
                log.Debug($"Ürünler getiriliyor...  {Assembly.GetAssembly(typeof(ProductController))}");
                var productTask = _manager.ProductService.GetAll(false);
                var products = await productTask;
                log.Debug($"{products.Count()} Adet Ürün Getirildi... {Assembly.GetAssembly(typeof(ProductController))} - Admin");

                var model = products.ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        public async Task<IActionResult> Create(){
            try
            {
                log.Debug($"Create View'i getiriliyor... {Assembly.GetAssembly(typeof(ProductController))}");
                ViewBag.Categories = await GetCategoriesSelectList();
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        private async Task<SelectList> GetCategoriesSelectList(){
            
            try
            {
                return new SelectList(await _manager.CategoryService.GetAll(false), "CategoryId", "CategoryName", 1);
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
                return new SelectList(new List<SelectListItem>(), "CategoryId", "CategoryName", ex.Message);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto, IFormFile file){
            try
            {
                if (ModelState.IsValid)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                    log.Debug($"Klasörün oluşturulacağı dosya yolu: {path} - {Assembly.GetAssembly(typeof(ProductController))} - Admin");
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    productDto.ImageUrl = String.Concat("/images/", file.FileName);
                    await _manager.ProductService.CreateOne(productDto);
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", "", ex.Message);
            }
        }
        public async Task<IActionResult> Update([FromRoute]int id){
            try
            {
                ViewBag.Categories = await GetCategoriesSelectList();
                var model = await _manager.ProductService.GetOneForUpdate(id, true);
                return View(model);
            }
            catch(Exception ex) 
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm]ProductDtoForUpdate productDto, IFormFile file){
            try
            {
                if (ModelState.IsValid)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName); //Proje sunucuda nerede yer alıyorsa o adrese git, sonrasında wwwroot/images/file.filename adresini kullan.
                    log.Debug($"Klasörün oluşturulacağı dosya yolu: {path} - {Assembly.GetAssembly(typeof(ProductController))} - Admin");
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    productDto.ImageUrl = String.Concat("/images/", file.FileName);
                    log.Debug($"Klasörün dosya yolu: {productDto.ImageUrl} - {Assembly.GetAssembly(typeof(ProductController))} - Admin");
                    await _manager.ProductService.UpdateOne(productDto);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Delete([FromRoute(Name = "id")]int id){
            try
            {
                log.Debug($"Delete View'i getiriliyor... - {Assembly.GetAssembly(typeof(ProductController))} - Admin");
                _manager.ProductService.DeleteOne(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
    }   
}