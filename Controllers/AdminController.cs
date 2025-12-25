using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using emlakdeneme.Models;
using System.Threading.Tasks;

namespace emlakdeneme.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Onay bekleyen ilanlar
        public async Task<IActionResult> IlanOnay()
        {
            var ilanlar = await _context.Ilanlar
                .Where(x => !x.Onaylandi)
                .ToListAsync();

            return View(ilanlar);
        }

        // Onayla
        public async Task<IActionResult> Onayla(int id)
        {
            var ilan = await _context.Ilanlar.FindAsync(id);
            if (ilan != null)
            {
                ilan.Onaylandi = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("IlanOnay");
        }
    }
}