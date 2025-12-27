using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using emlakdeneme.Models;
using emlakdeneme.Models.ViewModels;

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
            return PartialView("_SifreDegistir", new SifreDegistirViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SifreDegistir(SifreDegistirViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("KullaniciId");
            if (userId == null)
                return RedirectToAction("Login", "Kullanici");

            if (!ModelState.IsValid)
                return PartialView("_SifreDegistir", model);

            var user = _context.Kullanicilar.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return RedirectToAction("Login", "Kullanici");

            if (user.Sifre != model.EskiSifre)
            {
                ModelState.AddModelError("EskiSifre", "Mevcut şifre yanlış");
                return PartialView("_SifreDegistir", model);
            }

            user.Sifre = model.YeniSifre;
            _context.SaveChanges();

            TempData["Success"] = "Şifreniz başarıyla değiştirildi";

            return RedirectToAction("Index");
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
            // Veritabanı bağlamın (örn: _context) üzerinden kullanıcıyı bul
            var user = _context.Kullanicilar.FirstOrDefault(x => x.Id == model.Id);

            if (user != null)
            {
                user.AdSoyad = model.AdSoyad;
                user.Email = model.Email;
                user.Telefon = model.Telefon;
                user.Sehir = model.Sehir;
                user.Adres = model.Adres;

                _context.SaveChanges(); // Veritabanına kaydet
                return Content("success"); // JavaScript tarafındaki result === "success" kontrolü için
            }

            return Content("error");
        }

        public IActionResult EditProfileForm()
        {
            var userId = GetUserId();
            if (userId == null) return RedirectToAction("Login", "Kullanici");

            var user = _context.Kullanicilar.FirstOrDefault(u => u.Id == userId);
            return PartialView("_EditProfile", user);
        }
    }
}
