using LumiaPraktika.Areas.Admin.ViewModels;
using LumiaPraktika.DAL;
using LumiaPraktika.Models;
using LumiaPraktika.Utilities.Extentions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LumiaPraktika.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PortfolioController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Portfolio> portfolios = await _context.Portfolios.ToListAsync();
            return View(portfolios);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePortfolioVM portfolioVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!portfolioVM.Photo.ValideType("image/"))
            {
                ModelState.AddModelError("Photo", "sekil tipi uygn deyil");
                return View();
            }
            if (!portfolioVM.Photo.ValideSize(2 * 1024))
            {
                ModelState.AddModelError("Photo", "Sekil olcusu uygun deyil 2mb olmalidir");
                return View();
            }
            string filename = await portfolioVM.Photo.CreatFileAsync(_env.WebRootPath, "assets", "img", "portfolio");
            Portfolio portfolio = new Portfolio()
            {
                Image = filename,
                Name = portfolioVM.Name,
                Description = portfolioVM.Description,

            };


            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            Portfolio existed = await _context.Portfolios.FirstOrDefaultAsync(p => p.Id == id);
            if (existed == null)
            {
                return NotFound();
            }
            UpdatePortfolioVM portfolioVM = new UpdatePortfolioVM
            {
                Image = existed.Image,
                Name = existed.Name,
                Description = existed.Description

            };
            return View(portfolioVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdatePortfolioVM portfolioVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Portfolio existed = await _context.Portfolios.FirstOrDefaultAsync(p => p.Id == id);
            if (existed is null)
            {
                return NotFound();
            }
            if (portfolioVM.Photo is not null)
            {
                if (!portfolioVM.Photo.ValideType("image/"))
                {
                    ModelState.AddModelError("Photo", "Photo tipi uygun deyl");
                    return View();
                }
                if (!portfolioVM.Photo.ValideSize(2 * 1024))
                {
                    ModelState.AddModelError("Photo", "sekil olcusu uygun deyl");
                    return View();
                }
                string newImage = await portfolioVM.Photo.CreatFileAsync(_env.WebRootPath, "assets", "img", "portfolio");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "img", "portfolio");
                existed.Image = newImage;
            }
          
            existed.Name = portfolioVM.Name;
            existed.Description = portfolioVM.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
       
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Portfolio portfolio = await _context.Portfolios.FirstOrDefaultAsync(p => p.Id == id);
            if (portfolio is null)
            {
                return NotFound();
            }
            portfolio.Image.DeleteFile(_env.WebRootPath, "assets", "img", "portfolio");
            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
