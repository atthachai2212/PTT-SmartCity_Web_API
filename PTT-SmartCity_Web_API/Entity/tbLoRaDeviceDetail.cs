namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbLoRaDeviceDetail")]
    public partial class tbLoRaDeviceDetail
    {
        [Key]
        [StringLength(50)]
        public string DevEUI { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Details { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public virtual tbLoRaDevice tbLoRaDevice { get; set; }
    }
}
