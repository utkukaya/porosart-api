using porosartapi.model.ViewModel;
using porosartapi.model;
using Mapster;

namespace porosartapi.Services;

public class TeamService : BaseService
{
    public TeamService(AppDbContext dbContext)
    {
        _db = dbContext;
    }

    public ResponseVM<List<TeamVM>> GetTeams()
    {
        var teams = _db.Teams.ToList();
        if (teams != null)
            return new ResponseVM<List<TeamVM>>(teams.Adapt<List<TeamVM>
            >());
        else
            return new ResponseVM<List<TeamVM>>("Game content cannot be found.");
    }
    public async Task<ResponseVM> AddTeamMember(UploadTeamVM teamVM)
    {
        try
        {
            var file = teamVM.ImageFile;
            if (file == null || file.Length == 0)
                return new ResponseVM("No file uploaded");

            if (!Directory.Exists(_appSetting.ImageFilesPath))
                Directory.CreateDirectory(_appSetting.ImageFilesPath);

            string filePath = Path.Combine(_appSetting.ImageFilesPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


            var newTeamMember = teamVM.Adapt<TeamMember>();
            newTeamMember.ImageName = file.FileName;
            
            _db.Teams.Add(newTeamMember);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        catch (Exception ex)
        {
            return new ResponseVM(ex.Message);
        }
    }

    public ResponseVM DeleteTeamMember(int teamMemberId)
    {
        var teamMember = _db.Teams.FirstOrDefault(x => x.Id == teamMemberId);
        if (teamMember == null)
            return new ResponseVM("Team member not found.");
        var teamContent = _db.TeamContent.FirstOrDefault(x => x.TeamId == teamMember.Id);
        if(teamContent != null){
            _db.Remove(teamContent);
            _db.SaveChanges();
        }
        _db.Remove(teamMember);
        _db.SaveChanges();
        return new ResponseVM(true);
    }

    public ResponseVM UpdateTeamContent(TeamContentVM teamContentVM)
    {
        var teamContent = _db.TeamContent.FirstOrDefault(x => x.TeamId == teamContentVM.TeamId);
        if (teamContent != null)
        {
            teamContent.HTMLCode = teamContentVM.HTMLCode;
            _db.TeamContent.Update(teamContent);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        else
        {
            var newTeamContent = teamContentVM.Adapt<TeamContent>();
            _db.TeamContent.Add(newTeamContent);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
    }
     public ResponseVM<TeamContentVM> GetTeamContent(int teamId)
    {
        var teamContent = _db.TeamContent.FirstOrDefault(x => x.TeamId == teamId);
        if (teamContent != null)
            return new ResponseVM<TeamContentVM>(teamContent.Adapt<TeamContentVM>());
        else
            return new ResponseVM<TeamContentVM>("Team content cannot be found.");
    }

}