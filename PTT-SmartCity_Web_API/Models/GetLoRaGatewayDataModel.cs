using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GetLoRaGatewayDataModel
    {
        public GetLoRaGatewayData[] items { get; set; }
        public int totalItems { get; set; }
    }

    public class GetLoRaGatewayData 
    {
        public string GatewayEUI { get; set; }
        public string IP_Address { get; set; }
        public string Port { get; set; }
        public string API_Token { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class LoRaGatewayDataFilterOptions
    {
        public string deveui { get; set; }
        public int length { get; set; }
    }
}