using LumiaPraktika.DAL;
using LumiaPraktika.Models;
using LumiaPraktika.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LumiaPraktika.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _context;

        public PortfolioController(AppDbContext context)
        {
           _context = context;
        }
        public  async Task<IActionResult> Detail(int id)
        {
            if (id <= 0) return BadRequest();
            Portfolio portfolio = await _context.Portfolios.FirstOrDefaultAsync(p => p.Id == id);
            if (portfolio == null) return NotFound();
            List<Portfolio> portfolios= await _context.Portfolios.ToListAsync();
            PortfolioVM vM = new PortfolioVM()
            {
                Portfolios = portfolio,
                RelatedPortfolio = portfolios
            };

            return View(vM);
        }
    }
}
