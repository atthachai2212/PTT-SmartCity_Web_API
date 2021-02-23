namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbGarbageBin")]
    public partial class tbGarbageBin
    {
        [Key]
        [StringLength(50)]
        public string DevEUI { get; set; }

        [Required]
        [StringLength(50)]
        public string LocationID { get; set; }

        public int BinNo { get; set; }

        [Required]
        [StringLength(50)]
        public string BinColor { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
