using Microsoft.AspNetCore.Mvc;
using emlakdeneme.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace emlakdeneme.Controllers
{
  
    public class RolController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RolController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var roller = await _context.Roller.ToListAsync();
            return View(roller);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string ad)
        {
            var rol = new Rol { Ad = ad };
            _context.Roller.Add(rol);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }

}

