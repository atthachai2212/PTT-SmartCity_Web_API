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
        public static WasteBinDataModel LasC01Data(string Data)
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

        public static SensorHubDataModel Las501Data(string Data)
        {
            var dataArr = HelperService.SplitString(Data, 4).ToArray();
            var DicData = new Dictionary<string, float?>();
            var myDict = new Dictionary<string, string>
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
            foreach (var item in myDict)
            {
                foreach (var d in dataArr)
                {
                    if (item.Value.Equals(d))
                    {
                        float? dataArray;
                        var charItem = Convert.ToUInt16(HelperService.SplitString(item.Value, 1).ToArray().LastOrDefault());
                        var index = Array.IndexOf(dataArr, item.Value);
                        if (charItem != 4)
                        {
                            var dataA = dataArr.Skip(index + 1).Take(1).FirstOrDefault();
                            var arrR = string.Join("", HelperService.SplitString(dataA, 2).ToArray().Reverse());
                            dataArray = HelperService.HexToInt32(arrR);
                            DicData.Add(item.Key, dataArray);
                        }
                        else
                        {
                            dataArray = HelperService.HexToFloatingPoint(string.Join("", dataArr.Skip(index + 1).Take(2)));
                            DicData.Add(item.Key, dataArray);
                        }
                        break;
                    }
                }
            }

            var sensorHubData = new SensorHubDataModel()
            {

            };

            return sensorHubData;
        }

    }
}