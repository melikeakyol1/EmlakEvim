using emlakdeneme.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace emlakdeneme.Controllers
{
 
    public class OdemeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OdemeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Ödeme geçmişi
        public async Task<IActionResult> Index(int kullaniciId)
        {
            var odemeler = await _context.Odemeler
                .Where(o => o.KullaniciId == kullaniciId)
                .ToListAsync();

            return View(odemeler);
        }

        // Yeni ödeme ekle
        [HttpPost]
        public async Task<IActionResult> Create(int kullaniciId, decimal tutar, string odemeYontemi)
        {
            var odeme = new Odeme
            {
                KullaniciId = kullaniciId,
                Tutar = tutar,
                OdemeYontemi = odemeYontemi
            };

            _context.Odemeler.Add(odeme);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { kullaniciId });
        }
    }

}
