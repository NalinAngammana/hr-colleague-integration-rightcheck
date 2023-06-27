using ColleagueInt.RTW.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using ColleagueInt.RTW.Database.Seeding;

namespace ColleagueInt.RTW.Database
{
    public class RTWContext : DbContext
    {
        public RTWContext()
            : base()
        {
        }

        public RTWContext(DbContextOptions<RTWContext> options)
            : base(options)
        {
        }

        public DbSet<AppSettings> AppSetting { get; set; }
        public DbSet<Error> Error { get; set; }
        public DbSet<Colleague> Colleague { get; set; }
        public DbSet<Incident> Incident { get; set; }
        public DbSet<IncidentDetail> IncidentDetail { get; set; }
        public DbSet<FilterData> FilterData { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<InboundErrorLog> InboundErrorLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                    .AddJsonFile("appsettings.json");

                IConfiguration configuration = configurationBuilder.Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("RTWConsumerContext"));

                optionsBuilder.ConfigureWarnings(c => c.Log((RelationalEventId.CommandExecuting, LogLevel.Debug)));
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();
        }
    }
}
