using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Restaurant.Pages
{
    public class DemoModel : PageModel
    {
        public string? FullName => HttpContext?.Session?.GetString("name") ?? "No Name";
        public void OnGet()
        {
        }
        public void OnPost([FromForm] string name){
            HttpContext.Session.SetString("name",name);
        }
    }
}
