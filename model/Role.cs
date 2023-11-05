using System.ComponentModel.DataAnnotations;

namespace porosartapi.model;

public class Role
{
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }
}