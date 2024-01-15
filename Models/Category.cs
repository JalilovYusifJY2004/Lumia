namespace LumiaPraktika.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Department>? Departments { get; set; }
    }
}
