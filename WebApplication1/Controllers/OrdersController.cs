using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models.DBObjects;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Afișează comenzile utilizatorului logat
        public IActionResult Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user == null) return RedirectToAction("Login", "Account");

            var orders = _context.Orders
                .Where(o => o.IdUser == user.IdUser && o.TransactionType == "Bought")
                .ToList();

            return View(orders);
        }
    }
}
