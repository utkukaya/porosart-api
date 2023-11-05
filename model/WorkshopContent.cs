using System.ComponentModel.DataAnnotations;

namespace porosartapi.model;

public class WorkshopContent : BaseModel
{
    public int Id { get; set; }
    public int WorkshopId { get; set; }
    public string HTMLCode { get; set; }
}
