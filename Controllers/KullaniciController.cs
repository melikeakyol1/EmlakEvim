using Microsoft.AspNetCore.Mvc;
using emlakdeneme.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace emlakdeneme.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public KullaniciController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: /Kullanici/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Kullanici/Register
        [HttpPost]
        public async Task<IActionResult> Register(Kullanici model)
        {
            if (ModelState.IsValid)
            {
                _context.Kullanicilar.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        // GET: /Kullanici/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string sifre)
        {
            // Include(u => u.Rol) ekleyerek kullanıcının rol ismine erişiyoruz
            var user = await _context.Kullanicilar
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == email && u.Sifre == sifre);

            if (user != null)
            {
                HttpContext.Session.SetInt32("KullaniciId", user.Id);
                HttpContext.Session.SetString("KullaniciAd", user.AdSoyad);

                string rolAdi = user.Rol?.Ad ?? "Kullanıcı";
                HttpContext.Session.SetString("UserRole", rolAdi);

                if (rolAdi == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email veya şifre hatalı.");
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var smtpEmail = _configuration["Smtp:Email"];
                var smtpPassword = _configuration["Smtp:Password"];

                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(smtpEmail, smtpPassword),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 20000
                };

                var mail = new MailMessage
                {
                    From = new MailAddress(smtpEmail, "Emlak Sistemi"),
                    Subject = "Şifre Sıfırlama",
                    Body =
@"Merhaba,

Şifre sıfırlama talebiniz alınmıştır.

Eğer bu işlemi siz yapmadıysanız bu maili dikkate almayınız.

Emlak Sistemi"
                };

                mail.To.Add(email);
                client.Send(mail);

                ViewBag.Message = "Şifre sıfırlama maili başarıyla gönderildi.";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Mail gönderilemedi: " + ex.Message;
            }

            return View();
        }
    }
}
