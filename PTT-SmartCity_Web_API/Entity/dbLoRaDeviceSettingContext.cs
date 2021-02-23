using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PTT_SmartCity_Web_API.Entity
{
    public partial class dbLoRaDeviceSettingContext : DbContext
    {
        public dbLoRaDeviceSettingContext()
            : base("name=dbLoRaDeviceSettingContext")
        {
        }

        public virtual DbSet<tbGarbageBin> tbGarbageBin { get; set; }
        public virtual DbSet<tbLoRaAlertLog> tbLoRaAlertLog { get; set; }
        public virtual DbSet<tbLoRaDevice> tbLoRaDevice { get; set; }
        public virtual DbSet<tbLoRaDeviceDetail> tbLoRaDeviceDetail { get; set; }
        public virtual DbSet<tbLoRaDeviceList> tbLoRaDeviceList { get; set; }
        public virtual DbSet<tbLoRaGateway> tbLoRaGateway { get; set; }
        public virtual DbSet<vwLoRaAlertUpdate> vwLoRaAlertUpdate { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbLoRaAlertLog>()
                .Property(e => e.Time)
                .HasPrecision(0);

            modelBuilder.Entity<tbLoRaDevice>()
                .HasOptional(e => e.tbLoRaDeviceDetail)
                .WithRequired(e => e.tbLoRaDevice)
                .WillCascadeOnDelete();

            modelBuilder.Entity<vwLoRaAlertUpdate>()
                .Property(e => e.Time)
                .HasPrecision(0);
        }
    }
}
