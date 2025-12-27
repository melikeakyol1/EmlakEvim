using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using emlakdeneme.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

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
            // NAVBAR ARAMA
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

            // Fiyat filtreleme
            if (minFiyat.HasValue)
                ilanlar = ilanlar.Where(x => x.Fiyat >= minFiyat.Value);

            if (maxFiyat.HasValue)
                ilanlar = ilanlar.Where(x => x.Fiyat <= maxFiyat.Value);

            return View(await ilanlar.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var ilan = await _context.Ilanlar
                .Include(i => i.IlanResimler)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (ilan == null) return NotFound();

            ViewBag.Mesajlar = await _context.Mesajlar
                .Include(m => m.Gonderen)
                .Where(m => m.IlanId == id)
                .OrderByDescending(m => m.Tarih)
                .ToListAsync();

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

            ilan.KullaniciId = kullaniciId.Value;

            ilan.Onaylandi = false;

            _context.Ilanlar.Add(ilan);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("IlanOnay", "Admin");
        
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

            var dbIlan = await _context.Ilanlar.FindAsync(id);
            if (dbIlan == null) return NotFound();

            dbIlan.Baslik = ilan.Baslik;
            dbIlan.Durum = ilan.Durum;
            dbIlan.Tip = ilan.Tip;
            dbIlan.Sehir = ilan.Sehir;
            dbIlan.Semt = ilan.Semt;
            dbIlan.Mahalle = ilan.Mahalle;
            dbIlan.Fiyat = ilan.Fiyat;
            dbIlan.Metrekare = ilan.Metrekare;
            dbIlan.Oda = ilan.Oda;
            dbIlan.Resim = ilan.Resim;

            await _context.SaveChangesAsync();
            TempData["Success"] = "İlan başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));

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
    


[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IslemiYap(int ilanId, string islemTipi, string odemeTipi)
        {
            var ilan = await _context.Ilanlar.FindAsync(ilanId);
            if (ilan == null) return NotFound();

            var kullaniciId = HttpContext.Session.GetInt32("KullaniciId");
            if (kullaniciId == null)
            {
                TempData["Mesaj"] = "Lütfen giriş yapın.";
                return RedirectToAction("Login", "Kullanici"); // Login sayfasına yönlendir
            }

            if (islemTipi == "Satın Alma")
            {
                if (ilan.SatildiMi)
                {
                    TempData["Mesaj"] = "Bu ilan zaten satılmış!";
                    return RedirectToAction("Details", new { id = ilanId });
                }
                ilan.SatildiMi = true;
            }
            else if (islemTipi == "Kiralama")
            {
                if (ilan.KiralandiMi)
                {
                    TempData["Mesaj"] = "Bu ilan zaten kiralanmış!";
                    return RedirectToAction("Details", new { id = ilanId });
                }
                ilan.KiralandiMi = true;
            }

            // Ödeme Durumu
            var odemeDurumu = "Başarılı";

            var islem = new Islem
            {
                IlanId = ilanId,
                KullaniciId = kullaniciId.Value,
                IslemTipi = islemTipi,
                OdemeTipi = odemeTipi,
                OdemeDurumu = odemeDurumu,
                Tarih = DateTime.Now
            };

            _context.Islemler.Add(islem);
            await _context.SaveChangesAsync();

            TempData["Mesaj"] = $"{islemTipi} işlemi başarıyla yapıldı! Ödeme yöntemi: {odemeTipi}";
            return RedirectToAction("Details", new { id = ilanId });
        }
    }
}
