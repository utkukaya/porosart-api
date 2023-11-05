using System.ComponentModel.DataAnnotations;

namespace porosartapi.model;

public class Game : BaseModel
{
    public int Id {get; set;}
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageName { get; set; }
    public string ImageRatio { get; set; }
}