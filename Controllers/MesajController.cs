using Microsoft.AspNetCore.Mvc;
using emlakdeneme.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace emlakdeneme.Controllers
{
    public class MesajController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MesajController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mesaj/Gonder?ilanId=5
        [HttpGet]
        public IActionResult Gonder(int? ilanId)
        {
            if (ilanId == null) return NotFound();
            ViewBag.IlanId = ilanId;
            return View();
        }

        // POST: Mesaj/Gonder
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gonder(int ilanId, string icerik)
        {
            if (string.IsNullOrWhiteSpace(icerik))
            {
                TempData["Mesaj"] = "Mesaj boş olamaz!";
                return RedirectToAction("Details", "Ilan", new { id = ilanId });
            }

            var ilan = await _context.Ilanlar.FindAsync(ilanId);
            if (ilan == null) return NotFound();

            var gonderenId = HttpContext.Session.GetInt32("KullaniciId");
            if (gonderenId == null)
            {
                TempData["Mesaj"] = "Mesaj göndermek için giriş yapmalısınız.";
                return RedirectToAction("Login", "Kullanici");
            }

            var mesaj = new Mesaj
            {
                IlanId = ilanId,
                GonderenId = gonderenId.Value,
                AliciId = ilan.KullaniciId.Value,
                Icerik = icerik,
                Tarih = DateTime.Now
            };

            _context.Mesajlar.Add(mesaj);
            await _context.SaveChangesAsync();

            TempData["Mesaj"] = "Mesaj başarıyla gönderildi!";
            return RedirectToAction("Details", "Ilan", new { id = ilanId });
        }


        // GET: Mesaj/GelenMesajlar
        [HttpGet]
        public async Task<IActionResult> GelenMesajlar()
        {
            var kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var mesajlar = await _context.Mesajlar
                .Include(m => m.Gonderen)
                .Include(m => m.Ilan)
                .Where(m => m.AliciId == kullaniciId)
                .OrderByDescending(m => m.Tarih)
                .ToListAsync();

            return View(mesajlar);
        }
    }
}