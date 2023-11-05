namespace porosartapi.model.ViewModel;

public class UploadGameImagesVM
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public IFormFile ImageFile { get; set; }
    public string FileName { get; set; }
}
