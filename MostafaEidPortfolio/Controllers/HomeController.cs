using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MostafaEidPortfolio.Models;
using Microsoft.EntityFrameworkCore; // Required for database operations
using MostafaEidPortfolio.Data;   // Required for ApplicationDbContext

namespace MostafaEidPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        // The ApplicationDbContext is "injected" here by the framework
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // MODIFIED: Made this async to fetch data from the database
        public async Task<IActionResult> Index()
        {
            // Fetch the top 3 newest projects to feature on the homepage
            var featuredProjects = await _context.Projects
                                        // MODIFIED: Order by the newest creation date instead of ID
                                        .OrderByDescending(p => p.CreatedDate)
                                        .Take(3) // Takes only the top 3
                                        .ToListAsync();

            // Pass the list of projects to the View using ViewBag
            ViewBag.FeaturedProjects = featuredProjects;

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Projects()
        {
            var projects = await _context.Projects.ToListAsync();
            return View(projects);
        }

        // --- METHOD FOR PROJECT DETAILS ---
        // This action handles URLs like /Home/Details/5
        public async Task<IActionResult> Details(int id)
        {
            // Find the project in the database using the provided ID
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

            // If no project is found with that ID, return a "Not Found" page
            if (project == null)
            {
                return NotFound(); // This returns a standard 404 error page
            }

            // If the project is found, pass the specific project object to the Details view
            return View(project);
        }

        public async Task<IActionResult> Services()
        {
            var services = await _context.Services.ToListAsync();
            return View(services);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}