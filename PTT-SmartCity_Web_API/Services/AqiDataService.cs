using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class AqiDataService : IAqiDataService
    {
        private readonly dbLoRaSmartCityContext db;
        private readonly IEnvironmentService environmentService;
        public AqiDataService()
        {
            this.db = new dbLoRaSmartCityContext();
            this.environmentService = new EnvironmentService();
        }

        public GetAQIDataModel getAQIAllZone()
        {
            var Last24Hr = DateTime.Now.AddHours(-24);
            var Last8Hr = DateTime.Now.AddHours(-8);
            var PMLast24Hr = this.environmentService.environmentSensorItems.Select(m => new 
            {
                DateTime = m.Date + m.Time,
                DevEUI = m.DevEUI,
                GatewayEUI = m.GatewayEUI,
                PM1 = m.PM1,
                PM2_5 = m.PM2_5,
                PM10 = m.PM10,
            }).Where(x => x.DateTime >= Last24Hr).ToList();
            
            var O3Last8Hr = this.environmentService.environmentSensorItems.Select(m => new
            {
                DateTime = m.Date + m.Time,
                DevEUI = m.DevEUI,
                GatewayEUI = m.GatewayEUI,
                O3 = m.O3
            }).Where(x => x.DateTime >= Last8Hr).ToList();

            var tMix = Convert.ToDateTime("2020-12-15 15:15:54");
            var tMin = Convert.ToDateTime("2020-12-14 15:15:54");
            //var sensorLast24Hr = this.environmentService.environmentSensorItems.Select(m => new
            //{
            //    DateTime = m.Date + m.Time,
            //    DevEUI = m.DevEUI,
            //    GatewayEUI = m.GatewayEUI,
            //    PM1 = m.PM1,
            //    PM2_5 = m.PM2_5,
            //    PM10 = m.PM10,
            //}).Where(x => x.DateTime >= tMin && x.DateTime <= tMix).ToList();

            var PM_Seneor = PMLast24Hr.GroupBy(e => e.DevEUI, (key, g) => new { DevEUI = key, PM1_AVG_Zone = g.Average(x => x.PM1), PM25_AVG_Zone = g.Average(x => x.PM2_5), PM10_AVG_Zone = g.Average(x => x.PM10) }).ToList();
            var PM25_B12No01 = PM_Seneor.Where(x => x.DevEUI == "0004A30B0028932C".ToLower()).FirstOrDefault()?.PM25_AVG_Zone ?? 0;
            var PM25_B12No03 = PM_Seneor.Where(x => x.DevEUI == "0004A30B00EC0AE2".ToLower()).FirstOrDefault()?.PM25_AVG_Zone ?? 0;
            var PM25_B12No17 = PM_Seneor.Where(x => x.DevEUI == "0004A30B00EBA418".ToLower()).FirstOrDefault()?.PM25_AVG_Zone ?? 0;
            var PM25_B12No18_2 = PM_Seneor.Where(x => x.DevEUI == "0004a30b0022df4b".ToLower()).FirstOrDefault()?.PM25_AVG_Zone ?? 0;
            var PM25_B12No18_1 = PM_Seneor.Where(x => x.DevEUI == "0004a30b00ebf405".ToLower()).FirstOrDefault()?.PM25_AVG_Zone ?? 0;
            var PM25_AVG = PM_Seneor.Count > 0 ? PM_Seneor.Average(x => x.PM25_AVG_Zone) : -1 ;

            var PM10_B12No01 = PM_Seneor.Where(x => x.DevEUI == "0004A30B0028932C".ToLower()).FirstOrDefault()?.PM10_AVG_Zone ?? 0;
            var PM10_B12No03 = PM_Seneor.Where(x => x.DevEUI == "0004A30B00EC0AE2".ToLower()).FirstOrDefault()?.PM10_AVG_Zone ?? 0;
            var PM10_B12No17 = PM_Seneor.Where(x => x.DevEUI == "0004A30B00EBA418".ToLower()).FirstOrDefault()?.PM10_AVG_Zone ?? 0;
            var PM10_B12No18_2 = PM_Seneor.Where(x => x.DevEUI == "0004a30b0022df4b".ToLower()).FirstOrDefault()?.PM10_AVG_Zone ?? 0;
            var PM10_B12No18_1 = PM_Seneor.Where(x => x.DevEUI == "0004a30b00ebf405".ToLower()).FirstOrDefault()?.PM10_AVG_Zone ?? 0;
            var PM10_AVG = PM_Seneor.Count > 0 ? PM_Seneor.Average(x => x.PM10_AVG_Zone) : -1;

            var O3_Serser = O3Last8Hr.GroupBy(e => e.DevEUI, (key, g) => new { DevEUI = key, O3_AVG_Zone = g.Average(x => x.O3) }).ToList();
            var O3_AVG = O3Last8Hr.Count > 0 ? O3_Serser.Average(x => x.O3_AVG_Zone) : - 1;

            List<float> ValueAll = new List<float>();
            ValueAll.Add(Convert.ToSingle(O3_AVG.ToString("#,0.00")));
            ValueAll.Add(Convert.ToSingle(PM25_AVG.ToString("#,0.00")));
            ValueAll.Add(Convert.ToSingle(PM10_AVG.ToString("#,0.00")));

            List<int> AQIAll = new List<int>();
            AQIAll.Add(HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))));
            AQIAll.Add(HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))));
            AQIAll.Add(HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))));

            GetAQIDataModel getAQIAllData = new GetAQIDataModel()
            {
                Date = PMLast24Hr.FirstOrDefault().DateTime.ToString("yyyy-MM-dd"),
                Time = PMLast24Hr.FirstOrDefault().DateTime.ToString("HH:mm:ss"),
                Zone = "ALL",

                O3 = new GetAQIData
                {
                    Value = Convert.ToSingle(O3_AVG.ToString("#,0.00")),
                    AQI = HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))))["Color"]
                },
                PM2_5 = new GetAQIData
                {
                    Value = Convert.ToSingle(PM25_AVG.ToString("#,0.00")),
                    AQI = HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))))["Color"]
                },
                PM10 = new GetAQIData
                {
                    Value = Convert.ToSingle(PM10_AVG.ToString("#,0.00")),
                    AQI = HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))))["Color"]
                },
                CO = new GetAQIData
                {
                    Value = 0,
                    AQI = 0,
                    AQI_Level = "",
                    AQI_Color = ""
                },
                SO2 = new GetAQIData
                {
                    Value = 0,
                    AQI = 0,
                    AQI_Level = "",
                    AQI_Color = ""
                },
                NO2 = new GetAQIData
                {
                    Value = 0,
                    AQI = 0,
                    AQI_Level = "",
                    AQI_Color = ""
                },
                AQI = new GetAQIData
                {
                    Value = Convert.ToSingle(ValueAll.Max().ToString("#,0.00")),
                    AQI = Convert.ToInt32(AQIAll.Max()),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(Convert.ToInt32(AQIAll.Max()))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(Convert.ToInt32(AQIAll.Max()))["Color"]
                }
            };
            return getAQIAllData;
        }

        public GetAQIDataModel getAQICommunityZone()
        {
            var Last24Hr = DateTime.Now.AddHours(-24);
            var Last8Hr = DateTime.Now.AddHours(-8);
            var PMLast24Hr = this.environmentService.environmentSensorItems
                .Where(x =>
                    x.DevEUI.ToLower() == String.Format("0004a30b0028932c").ToLower() ||
                    x.DevEUI.ToLower() == String.Format("0004a30b00ec0ae2").ToLower())
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA00223").ToLower() || 
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA0025E").ToLower())
                .Select(m => new {
                    DateTime = m.Date + m.Time,
                    DevEUI = m.DevEUI,
                    GatewayEUI = m.GatewayEUI,
                    PM1 = m.PM1,
                    PM2_5 = m.PM2_5,
                    PM10 = m.PM10,
                })
                .Where(x => x.DateTime >= Last24Hr)
                .ToList();

            var O3Last8Hr = this.environmentService.environmentSensorItems
                .Where(x =>
                    x.DevEUI.ToLower() == String.Format("0004a30b0028932c").ToLower() ||
                    x.DevEUI.ToLower() == String.Format("0004a30b00ec0ae2").ToLower())
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA00223").ToLower() ||
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA0025E").ToLower())
                .Select(m => new {
                    DateTime = m.Date + m.Time,
                    DevEUI = m.DevEUI,
                    GatewayEUI = m.GatewayEUI,
                    O3 = m.O3
                })
                .Where(x => x.DateTime >= Last8Hr)
                .ToList();

            var PM_Seneor = PMLast24Hr.GroupBy(e => e.DevEUI, (key, g) => new { DevEUI = key, PM1_AVG_Zone = g.Average(x => x.PM1), PM25_AVG_Zone = g.Average(x => x.PM2_5), PM10_AVG_Zone = g.Average(x => x.PM10) }).ToList();
            var PM25_AVG = PM_Seneor.Count > 0 ? PM_Seneor.Average(x => x.PM25_AVG_Zone) : -1;
            var PM10_AVG = PM_Seneor.Count > 0 ? PM_Seneor.Average(x => x.PM10_AVG_Zone) : -1;

            var O3_Serser = O3Last8Hr.GroupBy(e => e.DevEUI, (key, g) => new { DevEUI = key, O3_AVG_Zone = g.Average(x => x.O3) }).ToList();
            var O3_AVG = O3Last8Hr.Count > 0 ? O3_Serser.Average(x => x.O3_AVG_Zone) : -1;


            List<float> ValueAll = new List<float>();
            ValueAll.Add(Convert.ToSingle(O3_AVG.ToString("#,0.00")));
            ValueAll.Add(Convert.ToSingle(PM25_AVG.ToString("#,0.00")));
            ValueAll.Add(Convert.ToSingle(PM10_AVG.ToString("#,0.00")));

            List<int> AQIAll = new List<int>();
            AQIAll.Add(HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))));
            AQIAll.Add(HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))));
            AQIAll.Add(HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))));

            GetAQIDataModel getAQIAllData = new GetAQIDataModel()
            {
                Date = PMLast24Hr.FirstOrDefault().DateTime.ToString("yyyy-MM-dd"),
                Time = PMLast24Hr.FirstOrDefault().DateTime.ToString("HH:mm:ss"),
                Zone = "Community",

                O3 = new GetAQIData
                {
                    Value = Convert.ToSingle(O3_AVG.ToString("#,0.00")),
                    AQI = HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))))["Color"]
                },
                PM2_5 = new GetAQIData
                {
                    Value = Convert.ToSingle(PM25_AVG.ToString("#,0.00")),
                    AQI = HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))))["Color"]
                },
                PM10 = new GetAQIData
                {
                    Value = Convert.ToSingle(PM10_AVG.ToString("#,0.00")),
                    AQI = HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))))["Color"]
                },
                CO = new GetAQIData
                {
                    Value = 0,
                    AQI = 0,
                    AQI_Level = "",
                    AQI_Color = ""
                },
                SO2 = new GetAQIData
                {
                    Value = 0,
                    AQI = 0,
                    AQI_Level = "",
                    AQI_Color = ""
                },
                NO2 = new GetAQIData
                {
                    Value = 0,
                    AQI = 0,
                    AQI_Level = "",
                    AQI_Color = ""
                },
                AQI = new GetAQIData
                {
                    Value = Convert.ToSingle(ValueAll.Max().ToString("#,0.00")),
                    AQI = Convert.ToInt32(AQIAll.Max()),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(Convert.ToInt32(AQIAll.Max()))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(Convert.ToInt32(AQIAll.Max()))["Color"]
                }
            };
            return getAQIAllData;
        }

        public GetAQIDataModel getAQIEducationZone()
        {
            var Last24Hr = DateTime.Now.AddHours(-24);
            var Last8Hr = DateTime.Now.AddHours(-8);
            var PMLast24Hr = this.environmentService.environmentSensorItems
                .Where(x =>
                    x.DevEUI.ToLower() == String.Format("0004a30b00ec0ae2").ToLower() ||
                    x.DevEUI.ToLower() == String.Format("0004a30b00eba418").ToLower())
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA0025E").ToLower() || 
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA00246").ToLower())
                .Select(m => new {
                    DateTime = m.Date + m.Time,
                    DevEUI = m.DevEUI,
                    GatewayEUI = m.GatewayEUI,
                    PM1 = m.PM1,
                    PM2_5 = m.PM2_5,
                    PM10 = m.PM10,
                })
                .Where(x => x.DateTime >= Last24Hr)
                .ToList();

            var O3Last8Hr = this.environmentService.environmentSensorItems
                .Where(x =>
                    x.DevEUI.ToLower() == String.Format("0004a30b00ec0ae2").ToLower() ||
                    x.DevEUI.ToLower() == String.Format("0004a30b00eba418").ToLower())
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA0025E").ToLower() || 
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA00246").ToLower())
                .Select(m => new {
                    DateTime = m.Date + m.Time,
                    DevEUI = m.DevEUI,
                    GatewayEUI = m.GatewayEUI,
                    O3 = m.O3
                })
                .Where(x => x.DateTime >= Last8Hr)
                .ToList();

            var PM_Seneor = PMLast24Hr.GroupBy(e => e.DevEUI, (key, g) => new { DevEUI = key, PM1_AVG_Zone = g.Average(x => x.PM1), PM25_AVG_Zone = g.Average(x => x.PM2_5), PM10_AVG_Zone = g.Average(x => x.PM10) }).ToList();
            var PM25_AVG = PM_Seneor.Count > 0 ? PM_Seneor.Average(x => x.PM25_AVG_Zone) : -1;
            var PM10_AVG = PM_Seneor.Count > 0 ? PM_Seneor.Average(x => x.PM10_AVG_Zone) : -1;

            var O3_Serser = O3Last8Hr.GroupBy(e => e.DevEUI, (key, g) => new { DevEUI = key, O3_AVG_Zone = g.Average(x => x.O3) }).ToList();
            var O3_AVG = O3Last8Hr.Count > 0 ? O3_Serser.Average(x => x.O3_AVG_Zone) : -1;

            List<float> ValueAll = new List<float>();
            ValueAll.Add(Convert.ToSingle(O3_AVG.ToString("#,0.00")));
            ValueAll.Add(Convert.ToSingle(PM25_AVG.ToString("#,0.00")));
            ValueAll.Add(Convert.ToSingle(PM10_AVG.ToString("#,0.00")));

            List<int> AQIAll = new List<int>();
            AQIAll.Add(HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))));
            AQIAll.Add(HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))));
            AQIAll.Add(HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))));

            GetAQIDataModel getAQIAllData = new GetAQIDataModel()
            {
                Date = PMLast24Hr.FirstOrDefault().DateTime.ToString("yyyy-MM-dd"),
                Time = PMLast24Hr.FirstOrDefault().DateTime.ToString("HH:mm:ss"),
                Zone = "Education",

                O3 = new GetAQIData
                {
                    Value = Convert.ToSingle(O3_AVG.ToString("#,0.00")),
                    AQI = HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))))["Color"]
                },
                PM2_5 = new GetAQIData
                {
                    Value = Convert.ToSingle(PM25_AVG.ToString("#,0.00")),
                    AQI = HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))))["Color"]
                },
                PM10 = new GetAQIData
                {
                    Value = Convert.ToSingle(PM10_AVG.ToString("#,0.00")),
                    AQI = HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))))["Color"]
                },
                CO = new GetAQIData
                {
                    Value = 0,
                    AQI = 0,
                    AQI_Level = "",
                    AQI_Color = ""
                },
                SO2 = new GetAQIData
                {
                    Value = 0,
                    AQI = 0,
                    AQI_Level = "",
                    AQI_Color = ""
                },
                NO2 = new GetAQIData
                {
                    Value = 0,
                    AQI = 0,
                    AQI_Level = "",
                    AQI_Color = ""
                },
                AQI = new GetAQIData
                {
                    Value = Convert.ToSingle(ValueAll.Max().ToString("#,0.00")),
                    AQI = Convert.ToInt32(AQIAll.Max()),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(Convert.ToInt32(AQIAll.Max()))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(Convert.ToInt32(AQIAll.Max()))["Color"]
                }
            };
            return getAQIAllData;
        }

        public GetAQIDataModel getAQIInnovationZone()
        {
            var Last24Hr = DateTime.Now.AddHours(-24);
            var Last8Hr = DateTime.Now.AddHours(-8);
            var PMLast24Hr = this.environmentService.environmentSensorItems
                .Where(x =>
                    x.DevEUI.ToLower() == String.Format("0004a30b00eba418").ToLower() ||
                    x.DevEUI.ToLower() == String.Format("0004a30b0022df4b").ToLower() ||
                    x.DevEUI.ToLower() == String.Format("0004a30b00ebf405").ToLower())
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA00246").ToLower() || 
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA0023F").ToLower() || 
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA0024F").ToLower())
                .Select(m => new {
                    DateTime = m.Date + m.Time,
                    DevEUI = m.DevEUI,
                    GatewayEUI = m.GatewayEUI,
                    PM1 = m.PM1,
                    PM2_5 = m.PM2_5,
                    PM10 = m.PM10,
                })
                .Where(x => x.DateTime >= Last24Hr)
                .ToList();

            var O3Last8Hr = this.environmentService.environmentSensorItems
                .Where(x =>
                    x.DevEUI.ToLower() == String.Format("0004a30b00eba418").ToLower() ||
                    x.DevEUI.ToLower() == String.Format("0004a30b0022df4b").ToLower() ||
                    x.DevEUI.ToLower() == String.Format("0004a30b00ebf405").ToLower())
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA00246").ToLower() || 
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA0023F").ToLower() || 
                //x.GatewayEUI.ToLower() == String.Format("000B78FFFEA0024F").ToLower())
                .Select(m => new {
                    DateTime = m.Date + m.Time,
                    DevEUI = m.DevEUI,
                    GatewayEUI = m.GatewayEUI,
                    O3 = m.O3
                })
                .Where(x => x.DateTime >= Last8Hr)
                .ToList();

            var PM_Seneor = PMLast24Hr.GroupBy(e => e.DevEUI, (key, g) => new { DevEUI = key, PM1_AVG_Zone = g.Average(x => x.PM1), PM25_AVG_Zone = g.Average(x => x.PM2_5), PM10_AVG_Zone = g.Average(x => x.PM10) }).ToList();
            var PM25_AVG = PM_Seneor.Count > 0 ? PM_Seneor.Average(x => x.PM25_AVG_Zone) : -1;
            var PM10_AVG = PM_Seneor.Count > 0 ? PM_Seneor.Average(x => x.PM10_AVG_Zone) : -1;

            var O3_Serser = O3Last8Hr.GroupBy(e => e.DevEUI, (key, g) => new { DevEUI = key, O3_AVG_Zone = g.Average(x => x.O3) }).ToList();
            var O3_AVG = O3Last8Hr.Count > 0 ? O3_Serser.Average(x => x.O3_AVG_Zone) : -1;

            List<float> ValueAll = new List<float>();
            ValueAll.Add(Convert.ToSingle(O3_AVG.ToString("#,0.00")));
            ValueAll.Add(Convert.ToSingle(PM25_AVG.ToString("#,0.00")));
            ValueAll.Add(Convert.ToSingle(PM10_AVG.ToString("#,0.00")));

            List<int> AQIAll = new List<int>();
            AQIAll.Add(HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))));
            AQIAll.Add(HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))));
            AQIAll.Add(HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))));

            GetAQIDataModel getAQIAllData = new GetAQIDataModel()
            {
                Date = PMLast24Hr.FirstOrDefault().DateTime.ToString("yyyy-MM-dd"),
                Time = PMLast24Hr.FirstOrDefault().DateTime.ToString("HH:mm:ss"),
                Zone = "Innovation",
                //O3 = new GetAQIData
                //{
                //    Value = O3Last8Hr.Count > 0 ? Convert.ToSingle(O3Last8Hr.Average(x => x.O3).ToString("#,0.00")) : -1,
                //    AQI = HelperService.Pollution_O3Cal(O3Last8Hr.Count > 0 ? Convert.ToSingle(O3Last8Hr.Average(x => x.O3).ToString("#,0.00")) : -1),
                //    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_O3Cal(O3Last8Hr.Count > 0 ? Convert.ToSingle(O3Last8Hr.Average(x => x.O3).ToString("#,0.00")) : -1))["Level"],
                //    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_O3Cal(O3Last8Hr.Count > 0 ? Convert.ToSingle(O3Last8Hr.Average(x => x.O3).ToString("#,0.00")) : -1))["Color"]
                //},
                //PM2_5 = new GetAQIData
                //{
                //    Value = PMLast24Hr.Count > 0 ? Convert.ToSingle(PMLast24Hr.Average(x => x.PM2_5).ToString("#,0.00")) : -1,
                //    AQI = HelperService.Pollution_PM25Cal(PMLast24Hr.Count > 0 ? Convert.ToSingle(PMLast24Hr.Average(x => x.PM2_5).ToString("#,0.00")) : -1),
                //    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM25Cal(PMLast24Hr.Count > 0 ? Convert.ToSingle(PMLast24Hr.Average(x => x.PM2_5).ToString("#,0.00")) : -1))["Level"],
                //    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM25Cal(PMLast24Hr.Count > 0 ? Convert.ToSingle(PMLast24Hr.Average(x => x.PM2_5).ToString("#,0.00")) : -1))["Color"]
                //},
                //PM10 = new GetAQIData
                //{
                //    Value = PMLast24Hr.Count > 0 ? Convert.ToSingle(PMLast24Hr.Average(x => x.PM10).ToString("#,0.00")) : -1,
                //    AQI = HelperService.Pollution_PM10Cal(PMLast24Hr.Count > 0 ? Convert.ToSingle(PMLast24Hr.Average(x => x.PM10).ToString("#,0.00")) : -1),
                //    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM10Cal(PMLast24Hr.Count > 0 ? Convert.ToSingle(PMLast24Hr.Average(x => x.PM10).ToString("#,0.00")) : -1))["Level"],
                //    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM10Cal(PMLast24Hr.Count > 0 ? Convert.ToSingle(PMLast24Hr.Average(x => x.PM10).ToString("#,0.00")) : -1))["Color"]
                //},
                //CO = new GetAQIData
                //{
                //    Value = 0,
                //    AQI = 0,
                //    AQI_Level = "",
                //    AQI_Color = ""
                //},
                //SO2 = new GetAQIData
                //{
                //    Value = 0,
                //    AQI = 0,
                //    AQI_Level = "",
                //    AQI_Color = ""
                //},
                //NO2 = new GetAQIData
                //{
                //    Value = 0,
                //    AQI = 0,
                //    AQI_Level = "",
                //    AQI_Color = ""
                //},
                //AQI = new GetAQIData
                //{
                //    Value = Convert.ToSingle(ValueAll.Max().ToString("#,0.00")),
                //    AQI = Convert.ToInt32(AQIAll.Max()),
                //    AQI_Level = HelperService.AQIPollutionLevel_Cal(Convert.ToInt32(AQIAll.Max()))["Level"],
                //    AQI_Color = HelperService.AQIPollutionLevel_Cal(Convert.ToInt32(AQIAll.Max()))["Color"]
                //}
                O3 = new GetAQIData
                {
                    Value = Convert.ToSingle(O3_AVG.ToString("#,0.00")),
                    AQI = HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_O3Cal(Convert.ToSingle(O3_AVG.ToString("#,0.00"))))["Color"]
                },
                PM2_5 = new GetAQIData
                {
                    Value = Convert.ToSingle(PM25_AVG.ToString("#,0.00")),
                    AQI = HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM25Cal(Convert.ToSingle(PM25_AVG.ToString("#,0.00"))))["Color"]
                },
                PM10 = new GetAQIData
                {
                    Value = Convert.ToSingle(PM10_AVG.ToString("#,0.00")),
                    AQI = HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(HelperService.Pollution_PM10Cal(Convert.ToSingle(PM10_AVG.ToString("#,0.00"))))["Color"]
                },
                CO = new GetAQIData
                {
                    Value = 0,
                    AQI = 0,
                    AQI_Level = "",
                    AQI_Color = ""
                },
                SO2 = new GetAQIData
                {
                    Value = 0,
                    AQI = 0,
                    AQI_Level = "",
                    AQI_Color = ""
                },
                NO2 = new GetAQIData
                {
                    Value = 0,
                    AQI = 0,
                    AQI_Level = "",
                    AQI_Color = ""
                },
                AQI = new GetAQIData
                {
                    Value = Convert.ToSingle(ValueAll.Max().ToString("#,0.00")),
                    AQI = Convert.ToInt32(AQIAll.Max()),
                    AQI_Level = HelperService.AQIPollutionLevel_Cal(Convert.ToInt32(AQIAll.Max()))["Level"],
                    AQI_Color = HelperService.AQIPollutionLevel_Cal(Convert.ToInt32(AQIAll.Max()))["Color"]
                }
            };
            return getAQIAllData;
        }
    }
}