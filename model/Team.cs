using System.ComponentModel.DataAnnotations;

namespace porosartapi.model;

public class TeamMember : BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageName { get; set; }

}