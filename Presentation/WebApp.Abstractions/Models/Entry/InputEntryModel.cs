using System.ComponentModel.DataAnnotations;
using WebApp.Abstractions.Attributes.Validation;

namespace WebApp.Abstractions.Models.Entry;

public class InputEntryModel
{
    [Required(ErrorMessage = ErrorMessages.Required)]
    [Display(Name = "Запись")]
    public required string Content { get; set; }
}