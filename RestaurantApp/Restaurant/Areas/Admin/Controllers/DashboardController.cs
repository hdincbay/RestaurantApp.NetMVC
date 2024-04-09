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
            // Gelen modelin doðruluðunu kontrol edin
            if (ModelState.IsValid)
            {
                // Kullanýcýyý doðrulamak için SignInManager'ý kullanýn
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Giriþ baþarýlýysa, isteðe baðlý olarak belirtilen yere yönlendirin
                    return RedirectToAction(nameof(DashboardController.Index));
                }
                // Kullanýcý kilitlendiðinde, hesap kitlenme uyarýsý vermek için gerekirse
                if (result.RequiresTwoFactor)
                {
                    // Ýki faktörlü kimlik doðrulamasý iþlemleri buraya gelebilir
                }
                // Kullanýcý parolasý yanlýþsa veya kullanýcý hesabý yoksa, hata mesajý göster
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Kullanýcý hesabý kilitlendi.");
                    return RedirectToAction(nameof(Lockout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz giriþ denemesi.");
                    return View(model);
                }
            }

            // Model geçerli deðilse, tekrar giriþ sayfasýný göster
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
                TempData["SuccessMessage"] = "Üyelik iþlemi baþarýyla gerçekleþmiþtir.";
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