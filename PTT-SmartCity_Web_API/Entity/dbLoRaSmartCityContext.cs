using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

namespace PTT_SmartCity_Web_API.Entity
{
    public partial class dbLoRaSmartCityContext : DbContext
    {
        public dbLoRaSmartCityContext(int year)
            : base(string.Format(ConfigurationManager.ConnectionStrings["dbLoRaSmartCityContext"].ConnectionString, year))
        {
        }

        public dbLoRaSmartCityContext()
            : base(string.Format(ConfigurationManager.ConnectionStrings["dbLoRaSmartCityContext"].ConnectionString, DateTime.Now.Year))
        {
        }

        public virtual DbSet<tbEnvironmentSensor> tbEnvironmentSensor { get; set; }
        public virtual DbSet<tbGarbageBinSensor> tbGarbageBinSensor { get; set; }
        public virtual DbSet<tbGPS> tbGPS { get; set; }
        public virtual DbSet<tbGPS_Realtime> tbGPS_Realtime { get; set; }
        public virtual DbSet<tbLoRaWAN> tbLoRaWAN { get; set; }
        public virtual DbSet<tbLoRaWAN_RealTime> tbLoRaWAN_RealTime { get; set; }
        public virtual DbSet<tbSensorHub> tbSensorHub { get; set; }
        public virtual DbSet<tbWasteBinSensor> tbWasteBinSensor { get; set; }
        public virtual DbSet<tbWaterLevelSensor> tbWaterLevelSensor { get; set; }
        public virtual DbSet<tbWaterQualitySensor> tbWaterQualitySensor { get; set; }
        public virtual DbSet<tbWeatherSensor> tbWeatherSensor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<tbEnvironmentSensor>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbGPS>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbGPS_Realtime>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbLoRaWAN>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbLoRaWAN_RealTime>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbSensorHub>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbWasteBinSensor>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbWaterLevelSensor>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbWaterQualitySensor>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbWeatherSensor>()
                .Property(e => e.Time)
                .HasPrecision(0);
        }
    }
}
