using LumiaPraktika.DAL;
using LumiaPraktika.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LumiaPraktika.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _context.Categories.Include(c => c.Departments).ToListAsync();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool result = await _context.Categories.AnyAsync(c => c.Name.Trim() == category.Name.Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "bu adda ad movcuddur");
                return View();
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id<=0)
            {
                return BadRequest();
            }
            Category category= await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category==null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Category existed = await _context.Categories.Include(c=>c.Departments).FirstOrDefaultAsync(c => c.Id == id);
            if (existed is null)
            {
                return NotFound();
            }
            bool result= await _context.Categories.AnyAsync(c=>c.Name.Trim() == category.Name.Trim());
            if (result)
            {
                ModelState.AddModelError("Name", "ad movcud deyl");
            }
            existed.Name= category.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if(id<=0) return BadRequest();
            Category existed= await _context.Categories.Include(c=>c.Departments).FirstOrDefaultAsync(c=>c.Id == id);
            if (existed is null)
            {
                return NotFound();
            }
            _context.Categories.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
