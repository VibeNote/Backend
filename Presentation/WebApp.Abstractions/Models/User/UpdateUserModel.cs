using System.ComponentModel.DataAnnotations;
using WebApp.Abstractions.Attributes.Validation;

namespace WebApp.Abstractions.Models.User;

public class UpdateUserModel
{
    [Required(ErrorMessage = ErrorMessages.Required)]
    [Display(Name = "Имя пользователя")]
    public required string Username { get; set; }
}