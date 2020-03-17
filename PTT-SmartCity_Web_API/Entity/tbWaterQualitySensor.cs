namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbWaterQualitySensor")]
    public partial class tbWaterQualitySensor
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

        public float WaterTemp { get; set; }

        public float DO { get; set; }

        public float DO_Cal { get; set; }

        public float? BATLevel { get; set; }

        public float? BATVolt { get; set; }

        public float RSSI { get; set; }

        public float SNR { get; set; }
    }
}
