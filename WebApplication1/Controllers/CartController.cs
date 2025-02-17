using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models.DBObjects;

namespace WebApplication1.Controllers
{
    [Authorize]  // Asigură că doar utilizatorii autentificați pot accesa coșul
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Cart
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // Adaugă produs în coș
        public IActionResult AddToCart(int id)
        {
            var product = _context.Comics.FirstOrDefault(c => c.IdComic == id);
            if (product == null) return NotFound();

            var cart = GetCart();
            cart.Add(product);
            SaveCart(cart);

            return RedirectToAction("Index");
        }

        // Elimină produs din coș
        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();
            var itemToRemove = cart.FirstOrDefault(c => c.IdComic == id);
            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        // Obține coșul din sesiune
        private List<Comic> GetCart()
        {
            var sessionCart = HttpContext.Session.GetString("Cart");
            return sessionCart == null ? new List<Comic>() : System.Text.Json.JsonSerializer.Deserialize<List<Comic>>(sessionCart);
        }

        // Salvează coșul în sesiune
        private void SaveCart(List<Comic> cart)
        {
            HttpContext.Session.SetString("Cart", System.Text.Json.JsonSerializer.Serialize(cart));
        }

        // Finalizează comanda
        public async Task<IActionResult> Checkout()
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                return RedirectToAction("Index");
            }

            // Obține user-ul autentificat
            var userEmail = User.FindFirstValue(ClaimTypes.Email);  // Preluăm email-ul utilizatorului logat
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Creăm și salvăm comanda în baza de date
            foreach (var item in cart)
            {
                var orderItem = new Order
                {
                    IdUser = user.IdUser,
                    IdComic = item.IdComic,
                    Quantity = 1, // Poți implementa și un sistem de cantitate în coș
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    OrderStatus = "Processing",
                    ShippingAddress = user.Address ?? "Adresa utilizatorului",
                    PaymentMethod = "Card",
                    TransactionType = "Bought"
                };
                _context.Orders.Add(orderItem);
            }

            await _context.SaveChangesAsync();

            // Golește coșul după finalizarea comenzii
            cart.Clear();
            SaveCart(cart);

            return View("CheckoutConfirmation");
        }
    }
}
