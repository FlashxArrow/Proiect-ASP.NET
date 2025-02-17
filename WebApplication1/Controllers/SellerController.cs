using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models.DBObjects;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "seller")]
    public class SellerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SellerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Seller Dashboard - Display only the products added by the seller
        public IActionResult Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var seller = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (seller == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var sellerProducts = _context.Comics
                .Where(c => c.IdAuthor == seller.IdUser)  // Ensure Seller can only see their own products
                .ToList();

            return View(sellerProducts);
        }

        // Page to add a new product
        public IActionResult AddProduct()
        {
            return View();
        }

        // Save a new product
        [HttpPost]
        public IActionResult AddProduct(Comic comic)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var seller = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (seller == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Assign Seller as the author
            comic.IdAuthor = seller.IdUser;
            _context.Comics.Add(comic);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Display product details (only if the seller owns the product)
        public IActionResult Details(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var seller = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (seller == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var product = _context.Comics.FirstOrDefault(c => c.IdComic == id && c.IdAuthor == seller.IdUser);

            if (product == null)
            {
                return Unauthorized();  // Prevents access to other sellers' products
            }

            return View(product);
        }

        // Display the edit form (only if the seller owns the product)
        public IActionResult Edit(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var seller = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (seller == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var product = _context.Comics.FirstOrDefault(c => c.IdComic == id && c.IdAuthor == seller.IdUser);

            if (product == null)
            {
                return Unauthorized();  // Prevents unauthorized access
            }

            return View(product);
        }

        // Handle edit form submission
        [HttpPost]
        public IActionResult Edit(Comic comic)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var seller = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (seller == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var existingProduct = _context.Comics.FirstOrDefault(c => c.IdComic == comic.IdComic && c.IdAuthor == seller.IdUser);

            if (existingProduct == null)
            {
                return Unauthorized();  // Prevents modifying another seller's product
            }

            // Update product details
            existingProduct.ComicName = comic.ComicName;
            existingProduct.Price = comic.Price;
            existingProduct.Stock = comic.Stock;
            existingProduct.ShortDescription = comic.ShortDescription;
            existingProduct.ImageUrl = comic.ImageUrl;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Delete a product (only if the seller owns it)
        public IActionResult Delete(int id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var seller = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (seller == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var product = _context.Comics.FirstOrDefault(c => c.IdComic == id && c.IdAuthor == seller.IdUser);

            if (product == null)
            {
                return Unauthorized();  // Prevents deleting another seller's product
            }

            _context.Comics.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
