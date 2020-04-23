namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbLoRaWAN_RealTime
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string DevEUI { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string GatewayEUI { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        [Required]
        [StringLength(50)]
        public string DevAddr { get; set; }

        public int RSSI { get; set; }

        public float SNR { get; set; }

        public int SF { get; set; }

        public int BW { get; set; }

        public float Freq { get; set; }

        public int UpCtr { get; set; }

        public int Size { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
