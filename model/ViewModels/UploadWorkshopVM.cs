namespace porosartapi.model.ViewModel;

public class UploadWorkshopVM
{
    public int Id { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string FileName { get; set; }
    public string ImageRatio { get; set; }

}