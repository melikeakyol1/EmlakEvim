using emlakdeneme.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace emlakdeneme.Controllers
{

    public class IlanResimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IlanResimController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int ilanId)
        {
            var resimler = await _context.IlanResimler
                .Where(r => r.IlanId == ilanId)
                .ToListAsync();
            return View(resimler);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int ilanId, string resimYolu)
        {
            var resim = new IlanResim
            {
                IlanId = ilanId,
                ResimYolu = resimYolu
            };
            _context.IlanResimler.Add(resim);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { ilanId });
        }
    }

}
