using PTT_SmartCity_Web_API.Models;
using PTT_SmartCity_Web_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public static class LoraDataService
    {
        public static WasteBinDataModel LAS_C01L_Sensor(string Data)
        {
            var dataArray = HelperService.SplitString(Data, 1).ToArray();
            var full = Convert.ToUInt16(dataArray.Take(1).FirstOrDefault());
            var flame = Convert.ToUInt16(dataArray.Skip(1).Take(1).FirstOrDefault());
            var fall = string.Join("", dataArray.Skip(2).Take(2));
            var airLavel = Convert.ToSingle(string.Join("", dataArray.Skip(4).Take(4)));
            var wasteBinData = new WasteBinDataModel()
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
                { "BattVolt", "3002" },
                { "BattCurr", "7002" },
                { "BattPercent", "b002" },
                { "BattTemp", "f002" },
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
                BatteryVolt = dicSensorData.SingleOrDefault(s => s.Key.Equals("BattVolt")).Value,
                BatteryCurrent = dicSensorData.SingleOrDefault(s => s.Key.Equals("BattCurr")).Value,
                BatteryPercent = dicSensorData.SingleOrDefault(s => s.Key.Equals("BattPercent")).Value,
                //BatteryTemp = dicSensorData?.SingleOrDefault(s => s.Key.Equals("BattTemp")).Value
                BatteryTemp = dicSensorData.SingleOrDefault(s => s.Key.Equals("BattTemp")).Value != null ?
                      Convert.ToSingle(HelperService.KtoCelsius(Convert.ToDouble(dicSensorData.SingleOrDefault(s => s.Key.Equals("BattTemp")).Value))) :
                      dicSensorData.SingleOrDefault(s => s.Key.Equals("BattTemp")).Value
            };
            return sensorHubData;
        }

        public static WaterSensorDataModel SSB_LW_APL_01_Sensor(string Data)
        {
            var dataArray = HelperService.SplitString(Data, 2).ToArray();
            var waterSensorData = new WaterSensorDataModel
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
                LEV_Cal = HelperService.HexToInt16(string.Join("", dataArray.Skip(20).Take(2).Reverse().ToArray())),
            };
            return waterSensorData;
        }
    }
}