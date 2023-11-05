
namespace porosartapi.model.ViewModel;

public class TeamVM : BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageName { get; set; }
    public IFormFile ImageFile { get; set; }

}