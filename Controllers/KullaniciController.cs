using Microsoft.AspNetCore.Mvc;
using emlakdeneme.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace emlakdeneme.Controllers
{


    public class KullaniciController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KullaniciController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Kullanici/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Kullanici/Register
        [HttpPost]
        public async Task<IActionResult> Register(Kullanici model)
        {
            if (ModelState.IsValid)
            {
                _context.Kullanicilar.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // GET: /Kullanici/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Kullanici/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string sifre)
        {
            var user = await _context.Kullanicilar
                .FirstOrDefaultAsync(u => u.Email == email && u.Sifre == sifre);

            if (user != null)
            {
                // Basit giriş işlemi (cookie veya identity yok)
                HttpContext.Session.SetInt32("KullaniciId", user.Id);
                HttpContext.Session.SetString("KullaniciAd", user.AdSoyad);
                return RedirectToAction("Index", "Home");

            }

            ModelState.AddModelError("", "Email veya şifre hatalı.");
            return View();
        }
        
        public IActionResult Logout()
    {
        HttpContext.Session.Clear(); // Session temizlenir
        return RedirectToAction("Index", "Home");
    }
}

}
