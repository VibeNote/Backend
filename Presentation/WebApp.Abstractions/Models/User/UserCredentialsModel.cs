using System.ComponentModel.DataAnnotations;
using WebApp.Abstractions.Attributes.Validation;

namespace WebApp.Abstractions.Models.User;

public class UserCredentialsModel
{
    [Required(ErrorMessage = ErrorMessages.Required)]
    [Display(Name = "Логин")]
    [DataType(DataType.Password)]
    public required string Login { get; set; }
    
    [Required(ErrorMessage = ErrorMessages.Required)]
    [Display(Name = "Пароль")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}