using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GetLoRaWANDataModel
    {
        public GetLoRaWANData[] items { get; set; }
        public int totalItems { get; set; }
    }

    public class GetLoRaWANData
    {
        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public string DevAddr { get; set; }

        public string DevEUI { get; set; }

        public string DevType { get; set; }

        public string GatewayEUI { get; set; }

        public int RSSI { get; set; }

        public float SNR { get; set; }

        public int SF { get; set; }

        public int BW { get; set; }

        public float Freq { get; set; }

        public int UpCtr { get; set; }

        public int Size { get; set; }

        public string Data { get; set; }
    }

    public class LoRaWANDataFilterOptions
    {
        public string deveui { get; set; }
        public int length { get; set; }
    }
}