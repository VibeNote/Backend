using System.ComponentModel.DataAnnotations;
using WebApp.Abstractions.Attributes.Validation;

namespace WebApp.Abstractions.Models.User;

public class RegisterUserModel
{
    [Required(ErrorMessage = ErrorMessages.Required)]
    [Display(Name = "Имя пользователя")]
    public required string Username { get; set; }
    
    [Required(ErrorMessage = ErrorMessages.Required)]
    [Display(Name = "Данные для входа")]
    public required UserCredentialsModel Credentials { get; set; }
}