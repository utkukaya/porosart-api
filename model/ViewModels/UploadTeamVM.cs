
namespace porosartapi.model.ViewModel;

public class UploadTeamVM : BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageName { get; set; }
    public int TeamContentId { get; set; }
    public IFormFile ImageFile { get; set; }

}