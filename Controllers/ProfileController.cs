using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using emlakdeneme.Models;

namespace emlakdeneme.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int? GetUserId()
        {
            return HttpContext.Session.GetInt32("KullaniciId");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProfilBilgilerim()
        {
            var userId = GetUserId();
            if (userId == null) return RedirectToAction("Login", "Kullanici");

            var user = _context.Kullanicilar.FirstOrDefault(x => x.Id == userId);
            return PartialView("_ProfilBilgilerim", user);
        }

        public IActionResult Ilanlarim()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var ilanlar = _context.Ilanlar
                .Where(i => i.KullaniciId == userId)
                .ToList();

            return PartialView("_Ilanlarim", ilanlar);
        }

        public IActionResult Favorilerim()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var favoriler = _context.Favoriler
                .Include(f => f.Ilan)
                .Where(f => f.KullaniciId == userId)
                .ToList();

            return PartialView("_Favorilerim", favoriler);
        }

        public IActionResult SifreDegistir()
        {
            return PartialView("_SifreDegistir");
        }
        // GET: /Profile/EditProfile
        public IActionResult EditProfile()
        {
            var userId = GetUserId();
            if (userId == null) return RedirectToAction("Login", "Kullanici");

            var user = _context.Kullanicilar.FirstOrDefault(u => u.Id == userId);
            return View(user); // normal view (partial değil)
        }

        [HttpPost]
        public IActionResult EditProfile(Kullanici model)
        {
            var userId = GetUserId();
            if (userId == null)
                return Content("error");

            var user = _context.Kullanicilar.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return Content("error");

            user.AdSoyad = model.AdSoyad;
            user.Email = model.Email;

            _context.SaveChanges();

            return Content("success");
        }

        // GET: partial view olarak formu yükle
        public IActionResult EditProfileForm()
        {
            var userId = GetUserId();
            if (userId == null) return RedirectToAction("Login", "Kullanici");

            var user = _context.Kullanicilar.FirstOrDefault(u => u.Id == userId);
            return PartialView("_EditProfile", user);
        }
    }
}