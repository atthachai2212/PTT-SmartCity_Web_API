using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class LoRaWANDeviceDataModel
    {
        [Required]
        [StringLength(50)]
        public string DevEUI { get; set; }

        [Required]
        [StringLength(50)]
        public string DevType { get; set; }

        [StringLength(50)]
        public string GatewayEUI { get; set; }

        [StringLength(50)]
        public string DevModel { get; set; }
    }
}