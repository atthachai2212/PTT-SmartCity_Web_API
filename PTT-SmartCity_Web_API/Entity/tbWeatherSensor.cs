namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbWeatherSensor")]
    public partial class tbWeatherSensor
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

        public float WindSpeed { get; set; }

        public float WindVane { get; set; }

        public float Rain { get; set; }

        public float Luminosity { get; set; }

        public float Battery { get; set; }

        public float RSSI { get; set; }

        public float SNR { get; set; }
    }
}
