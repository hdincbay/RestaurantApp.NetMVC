using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Controllers;

namespace Restaurant.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller{
        private readonly Cart _cart;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<DashboardController> _logger;
        public DashboardController(Cart cart, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILogger<DashboardController> logger)
        {
            _cart = cart;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index(){
            return View();
        }
        public IActionResult Orders()
        {
            var model = _cart.Lines.ToArray();
            return View(model);
        }
        public IActionResult Lockout()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm]LoginViewModel model)
        {
            // Gelen modelin do�rulu�unu kontrol edin
            if (ModelState.IsValid)
            {
                // Kullan�c�y� do�rulamak i�in SignInManager'� kullan�n
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Giri� ba�ar�l�ysa, iste�e ba�l� olarak belirtilen yere y�nlendirin
                    return RedirectToAction(nameof(DashboardController.Index));
                }
                // Kullan�c� kilitlendi�inde, hesap kitlenme uyar�s� vermek i�in gerekirse
                if (result.RequiresTwoFactor)
                {
                    // �ki fakt�rl� kimlik do�rulamas� i�lemleri buraya gelebilir
                }
                // Kullan�c� parolas� yanl��sa veya kullan�c� hesab� yoksa, hata mesaj� g�ster
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Kullan�c� hesab� kilitlendi.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ge�ersiz giri� denemesi.");
                    return View(model);
                }
            }

            // Model ge�erli de�ilse, tekrar giri� sayfas�n� g�ster
            return View(model);
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm]SignUpViewModel request)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var identityResult = await _userManager.CreateAsync(new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.Phone,
            }, request.Password);
            if(identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "�yelik i�lemi ba�ar�yla ger�ekle�mi�tir.";
                return RedirectToAction(nameof(DashboardController.SignUp));
            }
            foreach(IdentityError item in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return View();
        }
    }
}