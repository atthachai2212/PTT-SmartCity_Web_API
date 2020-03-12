namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbSensorHub")]
    public partial class tbSensorHub
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

        public float Humidity { get; set; }

        public float Temperature { get; set; }

        public float CO2 { get; set; }

        public float? BatteryVolt { get; set; }

        public float? BatteryCurrent { get; set; }

        public float? BatteryPercent { get; set; }

        public float? BatteryTemp { get; set; }

        public float RSSI { get; set; }

        public float SNR { get; set; }
    }
}
