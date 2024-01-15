namespace LumiaPraktika.Areas.Admin.ViewModels
{
    public class UpdatePortfolioVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public IFormFile? Photo { get; set; }   
    }
}
