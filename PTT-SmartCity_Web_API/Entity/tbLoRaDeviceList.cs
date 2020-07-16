namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbLoRaDeviceList")]
    public partial class tbLoRaDeviceList
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string DevEUI { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string GatewayEUI { get; set; }

        [Required]
        [StringLength(50)]
        public string DevAddr { get; set; }

        [Required]
        [StringLength(50)]
        public string DevType { get; set; }

        [StringLength(30)]
        public string DevModel { get; set; }

        [Required]
        [StringLength(10)]
        public string Activate { get; set; }

        [Required]
        [StringLength(10)]
        public string Class { get; set; }

        [Required]
        [StringLength(10)]
        public string Channel { get; set; }

        [Required]
        public string AppSKey { get; set; }

        [Required]
        public string NwkSkey { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
