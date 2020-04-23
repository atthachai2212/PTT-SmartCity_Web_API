namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbGPS_Realtime
    {
        [Key]
        [StringLength(50)]
        public string DevEUI { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        [Required]
        [StringLength(50)]
        public string GatewayEUI { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        [StringLength(100)]
        public string Emergency { get; set; }

        public float? Battery { get; set; }

        public float RSSI { get; set; }

        public float SNR { get; set; }
    }
}
