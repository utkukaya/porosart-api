namespace porosartapi.model.ViewModel;

public class UploadWorkshopImagesVM
{
    public int Id { get; set; }
    public int WorkshopId { get; set; }
    public IFormFile ImageFile { get; set; }
    public string FileName { get; set; }
}
