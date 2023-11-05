using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using porosartapi.model.ViewModel;
using porosartapi.model;
using porosartapi.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace porosartapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WorkshopController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly WorkshopService _workshopService;

        public WorkshopController(IWebHostEnvironment hostingEnvironment, WorkshopService workshopService)
        {
            _hostingEnvironment = hostingEnvironment;
            _workshopService = workshopService;
        }

        [HttpPost(Name = "ChangeWorkshop")]
        public async Task<ResponseVM> ChangeWorkshop([FromForm] UploadWorkshopVM uploadWorkshopVM)
        {
            return await _workshopService.ChangeWorkshop(uploadWorkshopVM);
        }
        [HttpPost(Name = "AddWorkshop")]
        public async Task<ResponseVM> AddWorkshop([FromForm] UploadWorkshopVM uploadWorkshopVM)
        {
            return await _workshopService.AddWorkshop(uploadWorkshopVM);
        }
        [HttpPost(Name = "AddWorkshopImage")]
        public async Task<ResponseVM> AddWorkshopImage([FromForm] UploadWorkshopImagesVM uploadWorkshopImagesVM)
        {
            return await _workshopService.AddWorkshopImage(uploadWorkshopImagesVM);
        }
 
        [HttpPost(Name = "RemoveWorkshopImages")]
        public ResponseVM RemoveWorkshopImages([FromBody] RemoveWorkshopImagesVM removeWorkshopImagesVM)
        {
            return _workshopService.RemoveWorkshopImages(removeWorkshopImagesVM);
        }
        [HttpDelete(Name = "RemoveWorkshopImage")]
        public ResponseVM RemoveWorkshopImage(int workshopId)
        {
            return _workshopService.RemoveWorkshopImage(workshopId);
        }
        [AllowAnonymous]
        [HttpGet(Name = "GetWorkshopImages")]
        public ResponseVM<List<WorkshopImages>> GetWorkshopImages(int workshopId)
        {
            return _workshopService.GetWorkshopImages(workshopId);
        }

        
        [AllowAnonymous]
        [HttpGet(Name = "GetWorkshops")]
        public ResponseVM<List<WorkshopVM>> GetWorkshops()
        {
            return _workshopService.GetWorkshops();
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetWorkshopContent")]
        public ResponseVM<WorkshopContentVM> GetWorkshopContent(int workshopId)
        {
            return _workshopService.GetWorkshopContent(workshopId);
        }

        [HttpDelete(Name = "RemoveWorkshop")]
        public ResponseVM RemoveWorkshop(int workshopId)
        {
            return _workshopService.RemoveWorkshop(workshopId);
        }

        [HttpPost(Name = "AddWorkshopContent")]
        public ResponseVM AddWorkshopContent([FromBody] WorkshopContentVM workshopContentVM)
        {
            return _workshopService.AddWorkshopContent(workshopContentVM);
        }

        [HttpPost(Name = "UpdateWorkshopContent")]
        public ResponseVM UpdateWorkshopContent([FromBody] WorkshopContentVM workshopContentVM)
        {
            return _workshopService.UpdateWorkshopContent(workshopContentVM);
        }
        
        
    }
}
