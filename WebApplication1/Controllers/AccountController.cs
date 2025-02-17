using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DBObjects;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Căutăm utilizatorul în baza de date
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    // Creăm un token de autentificare folosind cookie-uri
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role)  // Adăugăm rolul utilizatorului
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = model.RememberMe };

                    // Salvăm informațiile de autentificare în cookie-uri
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email sau parolă incorectă.");
            }
            return View(model);
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.Any(u => u.Email == model.Email);
                if (existingUser)
                {
                    ModelState.AddModelError("Email", "Acest email este deja utilizat.");
                    return View(model);
                }

                var newUser = new User
                {
                    Name = model.Username,
                    Email = model.Email,
                    Password = model.Password,  // Ar trebui să fie criptată (vezi mai jos)
                    Role = "buyer"  // Setăm rolul implicit ca "buyer"
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
