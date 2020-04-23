namespace PTT_SmartCity_Web_API.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbLoRaGateway")]
    public partial class tbLoRaGateway
    {
        [Key]
        [StringLength(50)]
        public string GatewayEUI { get; set; }

        [Required]
        [StringLength(32)]
        public string IP_Address { get; set; }

        [Required]
        [StringLength(10)]
        public string Port { get; set; }

        [Required]
        public string API_Token { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
