/*using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Station> Stations { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<WeatherData> WeatherData { get; set; }

        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=meteoBaza;Username=snezaBaza;Password=snezaBaza");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Station>()
                .HasMany(s => s.Sensors)
                .WithOne(sensor => sensor.Station)
                .HasForeignKey(sensor => sensor.StationId);

            modelBuilder.Entity<Station>()
                .HasMany(n => n.Nodes)
                .WithOne(node => node.Station)
                .HasForeignKey(node => node.StationId);
            modelBuilder.Entity<Sensor>()
                .HasMany(d => d.WeatherData)
                .WithOne(data => data.Sensor)
                .HasForeignKey(node => node.SensorId);
        }
    }
}*/