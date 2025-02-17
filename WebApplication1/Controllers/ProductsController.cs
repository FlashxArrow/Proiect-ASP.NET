using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.DBObjects;
using WebApplication1.Data;

namespace Proiect.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Products
        public async Task<IActionResult> Index(string searchString, string filter)
        {
            var comicsQuery = _context.Comics.Include(c => c.IdAuthorNavigation).AsQueryable();

            // Filtrare după numele produsului
            if (!string.IsNullOrEmpty(searchString))
            {
                comicsQuery = comicsQuery.Where(c => c.ComicName.Contains(searchString));
            }

            // Filtrare după franciza
            if (!string.IsNullOrEmpty(filter))
            {
                comicsQuery = comicsQuery.Where(c => c.IdFranchisesNavigation != null && c.IdFranchisesNavigation.FranchisesName.Contains(filter));
            }

            var comics = await comicsQuery.ToListAsync();

            // Trimite searchString și filter la ViewData
            ViewData["SearchString"] = searchString;
            ViewData["Filter"] = filter;

            return View(comics);
        }

        public async Task<IActionResult> Details(int id)
        {
            var comic = await _context.Comics.FirstOrDefaultAsync(c => c.IdComic == id);

            if (comic == null)
            {
                return NotFound();
            }

            return View(comic);
        }

    }
}
