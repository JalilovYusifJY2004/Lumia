using LumiaPraktika.Models;

namespace LumiaPraktika.Areas.Admin.ViewModels.Department
{
    public class UpdateDepartmentVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public IFormFile? Photo { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; }

    }
}
