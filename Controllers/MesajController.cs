using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using emlakdeneme.Models;

namespace emlakdeneme.Controllers
{
   

    public class MesajController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MesajController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Kullanıcının aldığı/gönderdiği mesajlar
        public async Task<IActionResult> Index(int kullaniciId)
        {
            var mesajlar = await _context.Mesajlar
                .Where(m => m.AliciId == kullaniciId || m.GonderenId == kullaniciId)
                .Include(m => m.Gonderen)
                .Include(m => m.Alici)
                .ToListAsync();

            return View(mesajlar);
        }

        // Mesaj gönder
        [HttpPost]
        public async Task<IActionResult> Create(int gonderenId, int aliciId, string icerik)
        {
            var mesaj = new Mesaj
            {
                GonderenId = gonderenId,
                AliciId = aliciId,
                Icerik = icerik
            };
            _context.Mesajlar.Add(mesaj);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { kullaniciId = gonderenId });
        }
    }

}

