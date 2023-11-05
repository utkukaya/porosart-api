using System.ComponentModel.DataAnnotations;

namespace porosartapi.model;

public class WorkshopImages : BaseModel
{
    public int Id { get; set; }
    public int WorkshopId { get; set; }
    public string ImageName { get; set; }
}
