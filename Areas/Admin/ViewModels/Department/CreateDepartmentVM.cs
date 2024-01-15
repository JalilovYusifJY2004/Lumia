using System.ComponentModel.DataAnnotations;

namespace LumiaPraktika.Areas.Admin.ViewModels.Department
{
    public class CreateDepartmentVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
        [Required]
        public string Description { get; set; }
        
        public int? CategoryId { get; set; }

    }
}
