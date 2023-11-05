using System.Security.Claims;
using Mapster;
using Newtonsoft.Json;
using porosartapi.model;
using porosartapi.model.BusinessModel;

namespace porosartapi.Services
{
    public class BaseService
    {
        public AppDbContext _db;
        public static string projectPath = Environment.CurrentDirectory;
        public static AppSettings _appSetting;
    }
}
