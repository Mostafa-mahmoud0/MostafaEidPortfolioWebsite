using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MostafaEidPortfolio.Models;
using Microsoft.EntityFrameworkCore;
using MostafaEidPortfolio.Data;
using System.Collections.Generic;

namespace MostafaEidPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var featuredProjects = await _context.Projects
                                        .OrderByDescending(p => p.CreatedDate)
                                        .Take(3)
                                        .ToListAsync();

            ViewBag.FeaturedProjects = featuredProjects;

            return View();
        }

        public IActionResult About()
        {
            var courses = new List<Course>
            {
                new Course
                {
                    Title = "CCNA (Cisco Certified Network Associate)",
                    Description = "I am currently studying the first third of the curriculum, focusing on network fundamentals, routing, switching, and subneting.",
                    Progress = 0.33
                },
                new Course
                {
                    Title = "Advanced .NET Backend Development",
                    Description = "Mastering the .NET ecosystem, including advanced Entity Framework Core, API design, and application security.",
                    Progress = 0.8
                }
            };

            return View(courses);
        }

        public async Task<IActionResult> Projects()
        {
            var projects = await _context.Projects.ToListAsync();
            return View(projects);
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

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