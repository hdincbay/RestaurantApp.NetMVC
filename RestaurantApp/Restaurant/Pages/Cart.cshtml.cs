using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Restaurant.Infrastructe.Extensions;

namespace Restaurant.Pages
{
    public class CartModel : PageModel
    {
        private readonly IServiceManager _manager;

        public CartModel(IServiceManager manager, Cart cartService)
        {
            _manager = manager;
            Cart = cartService;
        }
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/"; //Nerden geldiğini belirtir.
        public void OnGet(string returnUrl){
            ReturnUrl = returnUrl ?? "/"; //returnUrl boşsa "/", yani anasayfaya yönlendir.
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }
        public async Task<IActionResult> OnPost(int ProductId, string returnUrl){
            Product? product = await _manager.ProductService.GetOne(ProductId, false);
            if(product is not null){
                //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
                //HttpContext.Session.SetJson<Cart>("cart",Cart);
            }
            return RedirectToPage(new {
                returnUrl
            });
        }
        public IActionResult OnPostRemove(int ProductId, string returnUrl){
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.RemoveLine(Cart.Lines.First(l => l.Product.ProductId.Equals(ProductId)).Product);
            //HttpContext.Session.SetJson<Cart>("cart",Cart);
            return Page();
        }
    }
}