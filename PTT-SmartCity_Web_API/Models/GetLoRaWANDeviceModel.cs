using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GetLoRaWANDeviceModel
    {
        public GetLoRaWANDevice[] items { get; set; }
        public int totalItems { get; set; }
    }

    public class GetLoRaWANDevice
    {
        public string DevEUI { get; set; }
        public string DevType { get; set; }
        public string GatewayEUI { get; set; }
        public string DevModel { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }

    public class LoRaWANDeviceFilterOptions
    {
        [Required]
        public int startPage { get; set; }

        [Required]
        public int limitPage { get; set; }

        public string searchType { get; set; }
        public string searchText { get; set; }
    }
}