using System.ComponentModel.DataAnnotations;

namespace porosartapi.model;

public class GameContent : BaseModel
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public string HTMLCode { get; set; }
}
