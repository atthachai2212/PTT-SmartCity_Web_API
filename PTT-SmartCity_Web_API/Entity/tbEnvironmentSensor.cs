namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbEnvironmentSensor")]
    public partial class tbEnvironmentSensor
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

        public float O2 { get; set; }

        public float O3 { get; set; }

        public float PM1 { get; set; }

        [Column("PM2.5")]
        public float PM2_5 { get; set; }

        public float PM10 { get; set; }

        public float Battery { get; set; }

        public float RSSI { get; set; }

        public float SNR { get; set; }
    }
}
