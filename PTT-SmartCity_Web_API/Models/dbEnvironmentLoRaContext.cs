namespace PTT_SmartCity_Web_API.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Configuration;

    public partial class dbEnvironmentLoRaContext : DbContext
    {
        public dbEnvironmentLoRaContext()
            : base(string.Format(ConfigurationManager.ConnectionStrings["dbEnvironmentLoRaContext"].ConnectionString, DateTime.Now.Year))
        {
        }

        public virtual DbSet<tbEnvironmentSensor> tbEnvironmentSensor { get; set; }
        public virtual DbSet<tbLoRaGateway> tbLoRaGateway { get; set; }
        public virtual DbSet<tbSensorHub> tbSensorHub { get; set; }
        public virtual DbSet<tbWasteBinSensor> tbWasteBinSensor { get; set; }
        public virtual DbSet<tbWaterSensor> tbWaterSensor { get; set; }
        public virtual DbSet<tbWeatherSensor> tbWeatherSensor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbEnvironmentSensor>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbLoRaGateway>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbSensorHub>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbWasteBinSensor>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbWaterSensor>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbWeatherSensor>()
                .Property(e => e.Time)
                .HasPrecision(0);
        }
    }
}
