namespace porosartapi.model.ViewModel;
using System.ComponentModel.DataAnnotations;

public enum Roles
{
    Admin = 1,
    Editor,
    Labeler
}

public class UserVM
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string FullName { get; set; }

    [MinLength(7)]
    [Required(AllowEmptyStrings = false)]
    public string Username { get; set; }
    public string Phone { get; set; }

    [Required(AllowEmptyStrings = false)]
    public int RoleId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string RoleName { get; set; }
    public string Token { get; set; }
}