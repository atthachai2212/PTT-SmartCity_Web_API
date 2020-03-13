using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class WaterQualitySensorDataModel
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
        public Int16 DO { get; set; }
        public Int16 WT { get; set; }
        public Int16 DO_Cal { get; set; }
    }
}