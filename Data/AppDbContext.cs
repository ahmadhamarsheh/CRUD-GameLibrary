using GamesLibrary.Models;

namespace GamesLibrary.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Games> Games { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category {Id = 1 , Name = "Sport"},
                new Category {Id = 2 , Name = "Acion"},
                new Category {Id = 3 , Name = "Fighting"},
                new Category {Id = 4 , Name = "Racing"}
            });

            modelBuilder.Entity<Device>().HasData(new Device[]
            {
                new Device {Id = 1 , Name = "PlayStation", Icon = "bi bi-playstation"},
                new Device {Id = 2 , Name = "XBox", Icon = "bi bi-xbox"},
                new Device {Id = 3 , Name = "PC", Icon = "bi bi-pc-display"}
            });

            modelBuilder.Entity<GameDevice>().HasKey(e => new { e.GameId , e.DeviceId });
            base.OnModelCreating(modelBuilder);
        }

    }
}
