using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class EnvironmentSensorDataModel
    {
        public Int16 Type { get; set; }
        public Int16 ErrChk { get; set; }
        public Int16 BAT { get; set; }
        public Int16 BATVolt { get; set; }
        public Int16 ChgStat { get; set; }
        public Int16 ChgCur { get; set; }
        public Int16 ACC_X { get; set; }
        public Int16 ACC_Y { get; set; }
        public Int16 ACC_Z { get; set; }
        public Int16 O2conc { get; set; }
        public Int16 O3conc { get; set; }
        public Int16 PM1 { get; set; }
        public Int16 PM2_5 { get; set; }
        public Int16 PM10 { get; set; }
        public Int16 TC { get; set; }
        public Int16 HUM { get; set; }
        public Int16 PRES { get; set; }   

    }
}