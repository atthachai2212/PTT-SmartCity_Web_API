namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vwLoRaAlertUpdate")]
    public partial class vwLoRaAlertUpdate
    {
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public TimeSpan? Time { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AlertLevel { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Details { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }
    }
}
