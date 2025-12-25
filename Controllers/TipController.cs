using Microsoft.AspNetCore.Mvc;
using emlakdeneme.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace emlakdeneme.Controllers
{
   
    public class TipController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tipler = await _context.Tipler.ToListAsync();
            return View(tipler);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string ad)
        {
            var tip = new Tip { Ad = ad };
            _context.Tipler.Add(tip);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }

}

