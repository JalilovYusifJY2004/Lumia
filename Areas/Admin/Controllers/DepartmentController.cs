using Humanizer;
using LumiaPraktika.Areas.Admin.ViewModels.Department;
using LumiaPraktika.DAL;
using LumiaPraktika.Models;
using LumiaPraktika.Utilities.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LumiaPraktika.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Department> departments = await _context.Departments.Include(d => d.Category).ToListAsync();
            return View(departments);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentVM departmentVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                return View();
            }
            if (!departmentVM.Photo.ValideType("image/"))
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ModelState.AddModelError("Photo", "sekil tipi uygun deyil");
                return View();
            }
            if (!departmentVM.Photo.ValideSize(2 * 1024))
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ModelState.AddModelError("Photo", "sekil olcusu uygun deyil");
                return View();
            }
            string filename = await departmentVM.Photo.CreatFileAsync(_env.WebRootPath, "assets", "img", "team");
            Department department = new Department()
            {
                Image = filename,
                Name = departmentVM.Name,
                Description = departmentVM.Description,
                CategoryId = (int)departmentVM.CategoryId

            };
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Department? existed = await _context.Departments.Include(d => d.Category).FirstOrDefaultAsync(d => d.Id == id);
            if (existed is null)
            {
                return NotFound();
            }
            UpdateDepartmentVM vM = new UpdateDepartmentVM
            {
                Image = existed.Image,
                Name = existed.Name,
                Description = existed.Description,
                CategoryId = existed.CategoryId,
                Categories = await _context.Categories.ToListAsync()
            };
            return View(vM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateDepartmentVM vM)
        {
            if (ModelState.IsValid)
            {
                vM.Categories = await _context.Categories.ToListAsync();
                return View(vM);
            }
            Department existed = await _context.Departments.Include(d => d.Category).FirstOrDefaultAsync(d => d.Id == id);
            if (existed == null)
            {
                vM.Categories = await _context.Categories.ToListAsync();
                return View(vM);
            }
            if(vM.Photo is not null) 
            {
                if (!vM.Photo.ValideType("image/"))
                {
                    vM.Categories = await _context.Categories.ToListAsync();
                    ModelState.AddModelError("Photo", "sekil tipi uygun deyil");
                    return View(vM);
                }
                if (!vM.Photo.ValideSize(2 * 1024))
                {
                    vM.Categories = await _context.Categories.ToListAsync();
                    ModelState.AddModelError("Photo", "sekil tipi uygun deyil");
                    return View(vM);
                }
                string newImage = await vM.Photo.CreatFileAsync(_env.WebRootPath, "assets", "img", "team");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "img", "team");
                existed.Image = newImage;
            }

            existed.Name = vM.Name;
            existed.Description = vM.Description;
            existed.CategoryId = vM.CategoryId;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Department existed = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (existed is null)
            {
                return NotFound();
            }
            existed.Image.DeleteFile(_env.WebRootPath, "assets", "img", "team");
            _context.Departments.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
