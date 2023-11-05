using System.ComponentModel.DataAnnotations;

namespace porosartapi.model;

public class User : BaseModel
{
    public int Id {get; set;}
    [Required(AllowEmptyStrings = false)]
    public string FullName { get; set; }

    public string Phone { get; set; }

    [MinLength(7)]
    [Required(AllowEmptyStrings = false)]
    public string Username { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; }

    public int RoleId {get; set;}
    public Role Role {get; set; }
}