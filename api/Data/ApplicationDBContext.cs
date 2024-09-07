using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSet properties for your entities here
        public DbSet<Station> Stations { get; set; }
        public DbSet<WeatherStations> WeatherStations { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<WeatherData> WeatherData { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Station>()
                .HasKey(s => s.StationId);

            modelBuilder.Entity<Station>()
                .HasIndex(s => s.StationIdUuid)
                .IsUnique();

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

            modelBuilder.Entity<WeatherStations>()
                .HasMany(s => s.Stations)
                .WithOne(w => w.WeatherStation)
                .HasForeignKey(stat => stat.GeneratedAt);
        }
    }
}