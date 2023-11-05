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
    public class GameController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        private readonly GameService _gameService;

        public GameController(IWebHostEnvironment hostingEnvironment, GameService gameService)
        {
            _hostingEnvironment = hostingEnvironment;
            _gameService = gameService;
        }

        [HttpPost(Name = "ChangeGameCard")]
        public async Task<ResponseVM> ChangeGameCard([FromForm] UploadGameCardVM uploadGameCardVM)
        {
            return await _gameService.ChangeGameCard(uploadGameCardVM);
        }
        [HttpPost(Name = "AddGameCard")]
        public async Task<ResponseVM> AddGameCard([FromForm] UploadGameCardVM uploadGameCardVM)
        {
            return await _gameService.AddGameCard(uploadGameCardVM);
        }
        [HttpPost(Name = "AddGameImage")]
        public async Task<ResponseVM> AddGameImage([FromForm] UploadGameImagesVM uploadGameImagesVM)
        {
            return await _gameService.AddGameImage(uploadGameImagesVM);
        }
 
        [HttpPost(Name = "RemoveGameImages")]
        public ResponseVM RemoveGameImages([FromBody] RemoveGameImagesVM removeGameImagesVM)
        {
            return _gameService.RemoveGameImages(removeGameImagesVM);
        }
        [AllowAnonymous]
        [HttpGet(Name = "GetGameImages")]
        public ResponseVM<List<GameImages>> GetGameImages(int gameId)
        {
            return _gameService.GetGameImages(gameId);
        }

        
        [AllowAnonymous]
        [HttpGet(Name = "GetGameCards")]
        public ResponseVM<List<GameVM>> GetGameCards()
        {
            return _gameService.GetGameCards();
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetGameContent")]
        public ResponseVM<GameContentVM> GetGameContent(int gameId)
        {
            return _gameService.GetGameContent(gameId);
        }

        [HttpDelete(Name = "RemoveGameCard")]
        public ResponseVM RemoveGameCard(int cardId)
        {
            return _gameService.RemoveGameCard(cardId);
        }

        [HttpPost(Name = "AddGameContent")]
        public ResponseVM AddGameContent([FromBody] GameContentVM gameContentVM)
        {
            return _gameService.AddGameContent(gameContentVM);
        }

        [HttpPost(Name = "UpdateGameContent")]
        public ResponseVM UpdateGameContent([FromBody] GameContentVM gameContentVM)
        {
            return _gameService.UpdateGameContent(gameContentVM);
        }
        
        
    }
}
