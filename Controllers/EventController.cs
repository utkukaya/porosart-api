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
    public class EventController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly EventService _eventService;

        public EventController(IWebHostEnvironment hostingEnvironment, EventService eventService)
        {
            _hostingEnvironment = hostingEnvironment;
            _eventService = eventService;
        }

        [HttpPost(Name = "UpdateEvent")]
        public ResponseVM UpdateEvent([FromBody] EventVM uploadGameCardVM)
        {
            return _eventService.UpdateEvent(uploadGameCardVM);
        }
        [HttpPost(Name = "AddEvent")]
        public ResponseVM AddEvent([FromBody] EventVM uploadGameCardVM)
        {
            return _eventService.AddEvent(uploadGameCardVM);
        }
 
        [HttpDelete(Name = "RemoveEvent")]
        public ResponseVM RemoveEvent(int eventId)
        {
            return _eventService.RemoveEvent(eventId);
        }
        [AllowAnonymous]
        [HttpGet(Name = "GetEvents")]
        public ResponseVM<List<EventVM>> GetEvents()
        {
            return _eventService.GetEvents();
        }
    }
}
