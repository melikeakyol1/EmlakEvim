using emlakdeneme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace emlakdeneme.Controllers
{
    public class FavoriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoriController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ❤️ Favorilerim
        public async Task<IActionResult> Index()
        {
            int kullaniciId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var favoriler = await _context.Favoriler
                .Where(f => f.KullaniciId == kullaniciId)
                .Include(f => f.Ilan)
                .ToListAsync();

            return View(favoriler);
        }

        // 🤍 Favoriye ekle
        [HttpPost]
        public async Task<IActionResult> Add(int ilanId)
        {
            int? kullaniciId = HttpContext.Session.GetInt32("KullaniciId");

            if (kullaniciId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            bool varMi = await _context.Favoriler
                .AnyAsync(f => f.IlanId == ilanId && f.KullaniciId == kullaniciId);

            if (!varMi)
            {
                _context.Favoriler.Add(new Favori
                {
                    IlanId = ilanId,
                    KullaniciId = kullaniciId.Value
                });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Ilan", new { id = ilanId });

        }


        // ❤️ Favoriden kaldır
        [HttpPost]
        public async Task<IActionResult> Remove(int ilanId)
        {
            int? kullaniciId = HttpContext.Session.GetInt32("KullaniciId");

            if (kullaniciId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var favori = await _context.Favoriler
                .FirstOrDefaultAsync(f => f.IlanId == ilanId && f.KullaniciId == kullaniciId);

            if (favori != null)
            {
                _context.Favoriler.Remove(favori);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Detay", "Ilan", new { id = ilanId });
        }
    }
}
/*using emlakdeneme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emlakdeneme.Controllers
{
    public class FavoriController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoriController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int kullaniciId)
        {
            var favoriler = await _context.Favoriler
                .Where(f => f.KullaniciId == kullaniciId)
                .Include(f => f.Ilan)
                .ToListAsync();

            return View(favoriler);
        }


        public async Task<IActionResult> Add(int ilanId, int kullaniciId)
        {
            var favori = new Favori { IlanId = ilanId, KullaniciId = kullaniciId };
            _context.Favoriler.Add(favori);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Ilan");
        }
    }

}
*/