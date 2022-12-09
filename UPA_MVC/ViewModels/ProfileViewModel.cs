using System.ComponentModel.DataAnnotations;

namespace UPA_MVC.ViewModels;

public class ProfileViewModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FathersName { get; set; }
    public string Faculty { get; set; }
    public string Department { get; set; }
    public string Position { get; set; }
    public string CorporateEmail { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int countOfVacationsd { get; set; }
    public int countOfAssignments { get; set; }
}
