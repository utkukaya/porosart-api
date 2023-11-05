namespace porosartapi.model.ViewModel;
using System.ComponentModel.DataAnnotations;

public class LoginVM
{
    [MinLength(6)]
    [Required(AllowEmptyStrings = false)]
    public string Username { get; set; }

    [MinLength(8)]
    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; }
}