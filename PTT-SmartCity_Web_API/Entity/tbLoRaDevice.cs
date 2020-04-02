namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbLoRaDevice")]
    public partial class tbLoRaDevice
    {
        [Key]
        [StringLength(50)]
        public string DevEUI { get; set; }

        [Required]
        [StringLength(50)]
        public string DevType { get; set; }

        [StringLength(50)]
        public string GatewayEUI { get; set; }

        [StringLength(50)]
        public string DevModel { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }
    }
}
