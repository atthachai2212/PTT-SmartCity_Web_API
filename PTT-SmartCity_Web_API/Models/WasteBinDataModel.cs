using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class WasteBinDataModel
    {
        public bool Full { get; set; }
        public bool Flame { get; set; }
        public bool Fall { get; set; }
        public float AirLevel { get; set; }

    }
}