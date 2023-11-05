using Microsoft.EntityFrameworkCore;

namespace porosartapi.model.BusinessModel
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public DbContextOptions<AppDbContext> DbContextOption { get; set; }
        public string ImageFilesPath { get; set; }
    }

}
