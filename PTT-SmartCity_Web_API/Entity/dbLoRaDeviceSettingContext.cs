namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dbLoRaDeviceSettingContext : DbContext
    {
        public dbLoRaDeviceSettingContext()
            : base("name=dbLoRaDeviceSettingContext")
        {
        }

        public virtual DbSet<tbLoRaDevice> tbLoRaDevice { get; set; }
        public virtual DbSet<tbLoRaDeviceList> tbLoRaDeviceList { get; set; }
        public virtual DbSet<tbLoRaGateway> tbLoRaGateway { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
