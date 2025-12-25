using System.Diagnostics;
using emlakdeneme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace emlakdeneme.Controllers
{
 
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Slider verileri
            var slaytlar = await _context.Slaytlar.ToListAsync();

            // En yeni ilanlar (son eklenen 6 ilan)
            var yeniIlanlar = await _context.Ilanlar
                .OrderByDescending(i => i.Id)
                .Take(6)
                .ToListAsync();

            // ViewModel kullan
            var model = new HomeIndexViewModel
            {
                Slaytlar = slaytlar,
                YeniIlanlar = yeniIlanlar

            };

            return View(model);
        }
    }

}
