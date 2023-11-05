using Microsoft.EntityFrameworkCore;


namespace porosartapi.model
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {



        }
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameImages> GameImages{ get; set; }
        public DbSet<GameContent> GameContent { get; set; }
        public DbSet<TeamMember> Teams { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Workshop> Workshop { get; set; }
        public DbSet<WorkshopImages> WorkshopImages { get; set; }
        public DbSet<WorkshopContent> WorkshopContent { get; set; }
        public DbSet<TeamContent> TeamContent {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseNpgsql("User ID=porosart2607;Password=Porosart2607.;Server=porosart.cfe4mcvjer2y.us-east-1.rds.amazonaws.com;Port=5432;Database=postgres;Integrated Security=true;Pooling=true;");
            base.OnConfiguring(optionsBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
