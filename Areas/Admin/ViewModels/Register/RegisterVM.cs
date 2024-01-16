using System.ComponentModel.DataAnnotations;

namespace LumiaPraktika.Areas.Admin.ViewModels;

public class RegisterVM
{
    [Required]
    [MinLength(3, ErrorMessage = "minium 3 herf olmalidi")]
    public string Name { get; set; }

    [Required]
    [MinLength(3, ErrorMessage = "minium 3 herf olmalidi")]
    public string Surname { get; set; }

    [Required]
    [MinLength(3, ErrorMessage = "minium 3 herf olmalidi")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}
