using System.ComponentModel.DataAnnotations;

namespace porosartapi.model;

public class TeamContent : BaseModel
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public string HTMLCode { get; set; }
}
