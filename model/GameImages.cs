using System.ComponentModel.DataAnnotations;

namespace porosartapi.model;

public class GameImages : BaseModel
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public string ImageName { get; set; }
}
