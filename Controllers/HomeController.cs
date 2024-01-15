using LumiaPraktika.DAL;
using LumiaPraktika.Models;
using LumiaPraktika.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LumiaPraktika.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Department> department= await _context.Departments.Include(d=>d.Category).ToListAsync();
            List<Category> categories = await _context.Categories.ToListAsync();
            List<Portfolio> portfolios = await _context.Portfolios.ToListAsync();
            List<Service> services= await _context.Services.ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Portfolios = portfolios,
                Categories = categories,
                Departments = department,
                Services = services
            };
            
            return View(homeVM);
        }
    }
}
