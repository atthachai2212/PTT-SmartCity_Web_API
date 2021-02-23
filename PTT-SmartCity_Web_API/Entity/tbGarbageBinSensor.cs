namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbGarbageBinSensor")]
    public partial class tbGarbageBinSensor
    {
        [Key]
        [Column(Order = 0)]
        public DateTime DateTime { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string DevEUI { get; set; }

        [Required]
        [StringLength(50)]
        public string LocationID { get; set; }

        public int BinNo { get; set; }

        [Required]
        [StringLength(50)]
        public string BinColor { get; set; }

        public bool Full { get; set; }

        public bool Flame { get; set; }

        public float AirLevel { get; set; }

        public float RSSI { get; set; }

        public float SNR { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
