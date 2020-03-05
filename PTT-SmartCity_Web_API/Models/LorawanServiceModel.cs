using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class LorawanServiceModel
    {
        public string owner { get; set; }
        public string devname { get; set; }
        public string time { get; set; }
        public double millistime { get; set; }
        public string raw_data { get; set; }
        public int datasize { get; set; }
        public int fport { get; set; }
        public string devaddr { get; set; }
        public string deveui { get; set; }
        public int sf { get; set; }
        public int bw { get; set; }
        public float freq { get; set; }
        public int uplink_count { get; set; }
        public int rssi { get; set; }
        public float snr { get; set; }
        public string gateway_eui { get; set; }
        public string gateway_list { get; set; }
    }
}