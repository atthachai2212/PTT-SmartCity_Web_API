namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbGPS
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

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        [StringLength(100)]
        public string Emergency { get; set; }

        public float? Battery { get; set; }

        public float RSSI { get; set; }

        public float SNR { get; set; }
    }
}
