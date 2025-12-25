using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using emlakdeneme.Models;

namespace emlakdeneme.Controllers
{
    public class IlanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IlanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Ilan/Index
        public async Task<IActionResult> Index(
            string q,              
            string durum,
            string tip,
            string sehir,
            string semt,
            int? minFiyat,
            int? maxFiyat
        )
        {
            var ilanlar = _context.Ilanlar
    .Where(x => x.Onaylandi)
    .AsQueryable();
            // 🔍 NAVBAR ARAMA
            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.ToLower();

                ilanlar = ilanlar.Where(x =>
                    x.Baslik.ToLower().Contains(q) ||
                    x.Sehir.ToLower().Contains(q) ||
                    x.Semt.ToLower().Contains(q) ||
                    x.Tip.ToLower().Contains(q) ||
                    x.Durum.ToLower().Contains(q)
                );

                ViewBag.FiltreUygulandi = true;
            }

            // Filtreleme
            if (!string.IsNullOrEmpty(durum))
                ilanlar = ilanlar.Where(x => x.Durum.ToLower() == durum.ToLower());

            if (!string.IsNullOrEmpty(tip))
                ilanlar = ilanlar.Where(x => x.Tip.ToLower() == tip.ToLower());

            if (!string.IsNullOrEmpty(sehir))
                ilanlar = ilanlar.Where(x => x.Sehir.ToLower().Contains(sehir.ToLower()));

            if (!string.IsNullOrEmpty(semt))
                ilanlar = ilanlar.Where(x => x.Semt.ToLower().Contains(semt.ToLower()));

            // 💰 Fiyat filtreleme
            if (minFiyat.HasValue)
                ilanlar = ilanlar.Where(x => x.Fiyat >= minFiyat.Value);

            if (maxFiyat.HasValue)
                ilanlar = ilanlar.Where(x => x.Fiyat <= maxFiyat.Value);

            return View(await ilanlar.ToListAsync());
        }

        // GET: /Ilan/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ilan = await _context.Ilanlar.FirstOrDefaultAsync(i => i.Id == id);
            if (ilan == null) return NotFound();

            return View(ilan);
        }

        // GET: /Ilan/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ilan ilan)
        {
            var kullaniciId = HttpContext.Session.GetInt32("KullaniciId");

            if (kullaniciId == null)
                return RedirectToAction("Login", "Kullanici");

            if (ModelState.IsValid)
            {
                ilan.KullaniciId = kullaniciId.Value;
                ilan.Onaylandi = false;  // Admin onayı bekleyecek
                ilan.Kullanici = null;   // ⚡ Burayı ekledik, hata önlendi

                _context.Ilanlar.Add(ilan);
                await _context.SaveChangesAsync();

                // Admin onay sayfasına yönlendirelim
                return RedirectToAction("IlanOnay", "Admin");
            }

            return View(ilan);
        }
        // GET: /Ilan/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ilan = await _context.Ilanlar.FindAsync(id);
            if (ilan == null) return NotFound();
            return View(ilan);
        }

        // POST: /Ilan/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ilan ilan)
        {
            if (id != ilan.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(ilan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ilan);
        }

        // GET: /Ilan/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ilan = await _context.Ilanlar.FindAsync(id);
            if (ilan == null) return NotFound();
            return View(ilan);
        }

        // POST: /Ilan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ilan = await _context.Ilanlar.FindAsync(id);
            if (ilan != null)
            {
                var resimler = _context.IlanResimler.Where(r => r.IlanId == id);
                _context.IlanResimler.RemoveRange(resimler);

                var favoriler = _context.Favoriler.Where(f => f.IlanId == id);
                _context.Favoriler.RemoveRange(favoriler);

                _context.Ilanlar.Remove(ilan);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}