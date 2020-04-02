namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbWasteBinSensor")]
    public partial class tbWasteBinSensor
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime Date { get; set; }

        [Key]
        [Column(Order = 1)]
        public TimeSpan Time { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string DevEUI { get; set; }

        [Required]
        [StringLength(50)]
        public string GatewayEUI { get; set; }

        public bool Full { get; set; }

        public bool Flame { get; set; }

        public float AirLevel { get; set; }

        public float? Battery { get; set; }

        public float RSSI { get; set; }

        public float SNR { get; set; }
    }
}
