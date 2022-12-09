using System.ComponentModel.DataAnnotations;

namespace UPA_MVC.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Імя не вказано")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Прізвище не вказано")]
    public string Surname { get; set; }
    [Required(ErrorMessage = "По-батькові не вказано")]
    public string FathersName { get; set; }


    [Required(ErrorMessage = "Факультет не вказано")]
    public string Faculty { get; set; }
    [Required(ErrorMessage = "Кафедру не вказано")]
    public string Department { get; set; }
    [Required(ErrorMessage = "Посаду не вказано")]
    public string Position { get; set; }


    [Required(ErrorMessage = "Корпоративну пошту не вказано")]
    public string CorporateEmail { get; set; }

    [Required(ErrorMessage = "Персональну пошту не вказано")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Номер телефону не вказано")]
    public string PhoneNumber { get; set; }
}
