using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using porosartapi.model.ViewModel;
using porosartapi.Services;

namespace porosartapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TeamController : ControllerBase
    {

        private readonly TeamService _teamService;
        public TeamController(TeamService teamService)
        {
            _teamService = teamService;
        }


        [AllowAnonymous]
        [HttpGet(Name = "GetTeams")]
        public ResponseVM<List<TeamVM>> GetTeams()
        {
            return _teamService.GetTeams();
        }

        [HttpPost(Name = "AddTeamMember")]
        public async Task<ResponseVM> AddTeamMember([FromForm] UploadTeamVM teamVM)
        {
            return await _teamService.AddTeamMember(teamVM);
        }

        [HttpDelete(Name = "DeleteTeamMember")]
        public ResponseVM DeleteTeamMember(int teamMemberId)
        {
            return _teamService.DeleteTeamMember(teamMemberId);
        }

        [AllowAnonymous]
        [HttpGet(Name = "GetTeamContent")]
        public ResponseVM<TeamContentVM> GetTeamContent(int teamId)
        {
            return _teamService.GetTeamContent(teamId);
        }

        [HttpPost(Name = "UpdateTeamContent")]
        public ResponseVM UpdateTeamContent([FromBody] TeamContentVM teamContentVM)
        {
            return _teamService.UpdateTeamContent(teamContentVM);
        }
    }
}
