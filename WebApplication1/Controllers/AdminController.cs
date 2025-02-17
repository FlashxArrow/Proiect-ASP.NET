using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.DBObjects;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lista tuturor comenzilor
        public IActionResult Orders()
        {
            var allOrders = _context.Orders.ToList();
            return View(allOrders);
        }

        // Anulează o comandă
        public IActionResult CancelOrder(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.IdOrder == id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            return RedirectToAction("Orders");
        }
    }
}
