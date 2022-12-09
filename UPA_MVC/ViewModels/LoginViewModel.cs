using System.ComponentModel.DataAnnotations;

namespace UPA_MVC.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Не вказано Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Не вказано Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
