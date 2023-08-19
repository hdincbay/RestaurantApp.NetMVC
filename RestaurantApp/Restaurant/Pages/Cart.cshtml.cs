using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Restaurant.Pages
{
    public class CartModel : PageModel
    {
        private readonly IServiceManager _manager;

        public CartModel(IServiceManager manager, Cart cart)
        {
            _manager = manager;
            Cart = cart;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } //Nerden geldiğini belirtir.
        public void OnGet(string returnUrl){
            ReturnUrl = returnUrl ?? "/"; //returnUrl boşsa "/", yani anasayfaya yönlendir.
        }
        public IActionResult OnPost(int ProductId, string returnUrl){
            Product? product = _manager.ProductService.GetOne(ProductId, false);
            if(product is not null){
                Cart.AddItem(product, 1);
            }
            return Page();
        }
        public IActionResult OnPostRemove(int id, string returnUrl){
            Cart.RemoveLine(Cart.Lines.First(cl => cl.Product.ProductId.Equals(id)).Product);
            return Page();
        }
    }
}