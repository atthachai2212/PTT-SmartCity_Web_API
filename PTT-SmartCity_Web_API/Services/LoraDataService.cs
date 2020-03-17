using PTT_SmartCity_Web_API.Models;
using PTT_SmartCity_Web_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public static class LoRaDataService
    {
        public static WasteBinSensorDataModel LAS_C01L_Sensor(string Data)
        {
            var dataArray = HelperService.SplitString(Data, 1).ToArray();
            var full = Convert.ToUInt16(dataArray.Take(1).FirstOrDefault());
            var flame = Convert.ToUInt16(dataArray.Skip(1).Take(1).FirstOrDefault());
            var fall = string.Join("", dataArray.Skip(2).Take(2));
            var airLavel = Convert.ToSingle(string.Join("", dataArray.Skip(4).Take(4)));
            var wasteBinData = new WasteBinSensorDataModel()
            {
                Full = Convert.ToBoolean(full),
                Flame = Convert.ToBoolean(flame),
                Fall = false,
                AirLevel = airLavel
            };
            return wasteBinData;
        }

        public static SensorHubDataModel LAS_501L_Sensor(string Data)
        {
            var dataArray = HelperService.SplitString(Data, 4).ToArray();
            var dicSensorData = new Dictionary<string, float?>();
            var dicLasData = new Dictionary<string, string>
            {
                { "Temp", "0004" },
                { "Hum", "0104" },
                { "Air", "0204" },
                { "CO2", "0404" },
                { "CO", "0504" },
                { "PM25", "0604" },
                { "BATVolt", "3002" },
                { "BATCurrent", "7002" },
                { "BATLevel", "b002" },
                { "BATTemp", "f002" },
            };
            foreach (var item in dicLasData)
            {
                foreach (var data in dataArray)
                {
                    if (item.Value.Equals(data))
                    {
                        float? lasValue;
                        var arrayDataSize = Convert.ToUInt16(HelperService.SplitString(item.Value, 1).ToArray().LastOrDefault());
                        var index = Array.IndexOf(dataArray, item.Value);
                        if (arrayDataSize != 4)
                        {
                            var battData = dataArray.Skip(index + 1).Take(1).FirstOrDefault();
                            var hexData = string.Join("", HelperService.SplitString(battData, 2).Reverse().ToArray());
                            lasValue = HelperService.HexToInt32(hexData);
                            dicSensorData.Add(item.Key, lasValue);
                        }
                        else
                        {
                            lasValue = HelperService.HexToFloatingPoint(string.Join("", dataArray.Skip(index + 1).Take(2)));
                            dicSensorData.Add(item.Key, lasValue);
                        }
                        break;
                    }
                }
            }

            var sensorHubData = new SensorHubDataModel()
            {
                Humidity = Convert.ToSingle(dicSensorData.SingleOrDefault(s => s.Key.Equals("Hum")).Value),
                Temperature = Convert.ToSingle(dicSensorData.SingleOrDefault(s => s.Key.Equals("Temp")).Value),
                CO2 = Convert.ToSingle(dicSensorData.SingleOrDefault(s => s.Key.Equals("CO2")).Value),
                BATVolt = dicSensorData.SingleOrDefault(s => s.Key.Equals("BATVolt")).Value,
                BATCurrent = dicSensorData.SingleOrDefault(s => s.Key.Equals("BATCurrent")).Value,
                BATLevel = dicSensorData.SingleOrDefault(s => s.Key.Equals("BATLevel")).Value,
                //BatteryTemp = dicSensorData?.SingleOrDefault(s => s.Key.Equals("BattTemp")).Value
                BATTemp = dicSensorData.SingleOrDefault(s => s.Key.Equals("BATTemp")).Value != null ?
                      Convert.ToSingle(HelperService.KtoCelsius(Convert.ToDouble(dicSensorData.SingleOrDefault(s => s.Key.Equals("BATTemp")).Value))) :
                      dicSensorData.SingleOrDefault(s => s.Key.Equals("BATTemp")).Value
            };
            return sensorHubData;
        }

        public static WaterLevelSensorDataModel SSB_LW_APL_01_Sensor(string Data)
        {
            var dataArray = HelperService.SplitString(Data, 2).ToArray();
            var waterSensorData = new WaterLevelSensorDataModel()
            {
                Type = HelperService.HexToInt16(string.Join("",dataArray.Take(2).Reverse().ToArray())),
                ErrChk = HelperService.HexToInt16(string.Join("", dataArray.Skip(2).Take(2).Reverse().ToArray())),
                BAT = HelperService.HexToInt16(string.Join("", dataArray.Skip(4).Take(2).Reverse().ToArray())),
                BATVolt = HelperService.HexToInt16(string.Join("", dataArray.Skip(6).Take(2).Reverse().ToArray())),
                ChgStat = HelperService.HexToInt16(string.Join("", dataArray.Skip(8).Take(2).Reverse().ToArray())),
                ChgCur = HelperService.HexToInt16(string.Join("", dataArray.Skip(10).Take(2).Reverse().ToArray())),
                ACC_X = HelperService.HexToInt16(string.Join("", dataArray.Skip(12).Take(2).Reverse().ToArray())),
                ACC_Y = HelperService.HexToInt16(string.Join("", dataArray.Skip(14).Take(2).Reverse().ToArray())),
                ACC_Z = HelperService.HexToInt16(string.Join("", dataArray.Skip(16).Take(2).Reverse().ToArray())),
                USdist = HelperService.HexToInt16(string.Join("", dataArray.Skip(18).Take(2).Reverse().ToArray())),
                LEV_Cal = HelperService.HexToInt16(string.Join("", dataArray.Skip(20).Take(2).Reverse().ToArray()))
            };
            return waterSensorData;
        }

        public static WaterQualitySensorDataModel SWB_LW_APL_01_Sensor(string Data)
        {
            var waterQualitySensorData = new WaterQualitySensorDataModel()
            {
                Type = GetDataLibeliumSensor(Data,0,2),
                ErrChk = GetDataLibeliumSensor(Data,2,2),
                BAT = GetDataLibeliumSensor(Data, 4, 2),
                BATVolt = GetDataLibeliumSensor(Data, 6, 2),
                ChgStat = GetDataLibeliumSensor(Data, 8, 2),
                ChgCur = GetDataLibeliumSensor(Data, 10, 2),
                ACC_X = GetDataLibeliumSensor(Data, 12, 2),
                ACC_Y = GetDataLibeliumSensor(Data, 14, 2),
                ACC_Z = GetDataLibeliumSensor(Data, 16, 2),
                DO = GetDataLibeliumSensor(Data, 18, 2),
                WT = GetDataLibeliumSensor(Data, 20, 2),
                DO_Cal = GetDataLibeliumSensor(Data, 22, 2)
            };
            return waterQualitySensorData;    
        }


        public static WeatherSensorDataModel SAPB_LW_APL_01_Sensor(string Data)
        {
            var weatherSensorData = new WeatherSensorDataModel()
            {
                Type = GetDataLibeliumSensor(Data, 0, 2),
                ErrChk = GetDataLibeliumSensor(Data, 2, 2),
                BAT = GetDataLibeliumSensor(Data, 4, 2),
                BATVolt = GetDataLibeliumSensor(Data, 6, 2),
                ChgStat = GetDataLibeliumSensor(Data, 8, 2),
                ChgCur = GetDataLibeliumSensor(Data, 10, 2),
                ACC_X = GetDataLibeliumSensor(Data, 12, 2),
                ACC_Y = GetDataLibeliumSensor(Data, 14, 2),
                ACC_Z = GetDataLibeliumSensor(Data, 16, 2),
                ANE = GetDataLibeliumSensor(Data, 18, 2),
                WV = GetDataLibeliumSensor(Data, 20, 2),
                PLV1 = GetDataLibeliumSensor(Data, 22, 2),
                PLV2 = GetDataLibeliumSensor(Data, 24, 2),
                PLV3 = GetDataLibeliumSensor(Data, 26, 2),
                LUX = GetDataLibeliumSensor(Data, 28, 2)
            };
            return weatherSensorData;
        }

        public static EnvironmentSensorDataModel SEPB_LW_APL_01_Sensor(string Data)
        {
            var environmentSensorData = new EnvironmentSensorDataModel()
            {
                Type = GetDataLibeliumSensor(Data, 0, 2),
                ErrChk = GetDataLibeliumSensor(Data, 2, 2),
                BAT = GetDataLibeliumSensor(Data, 4, 2),
                BATVolt = GetDataLibeliumSensor(Data, 6, 2),
                ChgStat = GetDataLibeliumSensor(Data, 8, 2),
                ChgCur = GetDataLibeliumSensor(Data, 10, 2),
                ACC_X = GetDataLibeliumSensor(Data, 12, 2),
                ACC_Y = GetDataLibeliumSensor(Data, 14, 2),
                ACC_Z = GetDataLibeliumSensor(Data, 16, 2),
                O2conc = GetDataLibeliumSensor(Data, 18, 2),
                O3conc = GetDataLibeliumSensor(Data, 20, 2),
                PM1 = GetDataLibeliumSensor(Data, 22, 2),
                PM2_5 = GetDataLibeliumSensor(Data, 24, 2),
                PM10 = GetDataLibeliumSensor(Data, 26, 2),
                TC = GetDataLibeliumSensor(Data, 28, 2),
                HUM = GetDataLibeliumSensor(Data, 30, 2),
                PRES = GetDataLibeliumSensor(Data, 32, 2)
            };
            return environmentSensorData;
        }

        public static Int16 GetDataLibeliumSensor(string strData, ushort skip, ushort take)
        {
            var dataArray = HelperService.SplitString(strData, 2).ToArray();
            var data = HelperService.HexToInt16(string.Join("", dataArray.Skip(skip).Take(take).Reverse().ToArray()));
            return data;
        }
    }
}