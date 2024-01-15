using System.ComponentModel.DataAnnotations;

namespace LumiaPraktika.Areas.Admin.ViewModels
{
    public class CreatePortfolioVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
