using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class BusTrackingService : IBusTrackingService
    {
        private dbSmartCityContext db = new dbSmartCityContext();

        public IEnumerable<tbGPS_Realtime> GPS_ItemsRealtime => this.db.tbGPS_Realtime.ToList();

        public IEnumerable<tbGPS> GPS_Items => this.db.tbGPS.ToList();

        public IEnumerable<GPSModel> gpsItemsAll => this.GPS_Items.Select(g => new GPSModel
        {
            Date = g.Date,
            Time = g.Time,
            DevEUI = g.DevEUI,
            Latitude = g.Latitude,
            Longitude = g.Longitude,
            Emergency = g.Emergency,
            Battery = g.Battery,
            RSSI = g.RSSI,
            SNR = g.SNR
        }).OrderByDescending(g => g.Date).ThenBy(g => g.Time);

        public IEnumerable<GPSModel> gpsItemsRealtime => this.GPS_ItemsRealtime.Select(g => new GPSModel
        {
            Date = g.Date,
            Time = g.Time,
            DevEUI = g.DevEUI,
            Latitude = g.Latitude,
            Longitude = g.Longitude,
            Emergency = g.Emergency,
            Battery = g.Battery,
            RSSI = g.RSSI,
            SNR = g.SNR
        }).OrderByDescending(g => g.Date).ThenBy(g => g.Time);

        public GetGPSModel GetGPS(gpsFilterOptions filters)
        {
            var items = this.GPS_Items.Select(g => new GetGPS
            {
                Date = g.Date,
                Time = g.Time,
                DevEUI = g.DevEUI,
                Latitude = g.Latitude,
                Longitude = g.Longitude,
                Emergency = g.Emergency,
                Battery = g.Battery,
                RSSI = g.RSSI,
                SNR = g.SNR
            }).OrderByDescending(g => g.Date).ThenBy(g => g.Time);

            var startDate = filters.startDate ?? DateTime.Now;
            var limitDate = filters.limitDate ?? DateTime.Now;
            var length = filters.length ?? 100;
            var gpsItems = new GetGPSModel
            {
                items = items
                         .Take(length)
                         .ToArray(),
                totalItems = items.Count()
            };

            ////ตรวจสอบการค้นหาข้อมูลจากวันที่ หากมีเข้ามาก็จะเรียบเรียงข้อมูลใหม่
            //if (!string.IsNullOrEmpty(filters.searchType) && filters.searchType.Equals("updated"))
            //{
            //    var paramItem = HttpContext.Current.Request.Params;
            //    var fromDate = paramItem.Get("searchText[from]").Replace(" GMT+0700 (Indochina Time)", "");
            //    var toDate = paramItem.Get("searchText[to]").Replace(" GMT+0700 (Indochina Time)", "");
            //    filters.searchText = $"{fromDate},{toDate}";
            //}

            //หากว่ามีการค้นหาข้อมูลค้นหาในระบบ
            if (!string.IsNullOrEmpty(filters.searchType))
            {
                string searchText = filters.searchText;
                string searchType = filters.searchType;
                string formDate = filters.startDate.ToString() ?? DateTime.Now.ToString();
                string toDate = filters.limitDate.ToString() ?? DateTime.Now.ToString();
                IEnumerable<GetGPS> searchItem = new GetGPS[] { };

                switch (searchType)
                {
                    //ค้นหาจากวันที่
                    case "date":
                        DateTime FromDate = DateTime.Parse(formDate);
                        DateTime ToDate = DateTime.Parse(toDate);

                        searchItem = from m in items
                                     where m.Date >= FromDate && m.Date <= ToDate
                                     select m;
                        break;

                    //ค้นหาทั่วไป
                    default:
                        searchItem = from m in items
                                     where m.GetType()
                                     .GetProperty(filters.searchType)
                                     .GetValue(m)
                                     .ToString()
                                     .ToLower()
                                     .Contains(searchText.ToLower())
                                     select m;
                        break;
                }

                gpsItems.items = searchItem
                                .Take(length)
                                .ToArray();
                gpsItems.totalItems = searchItem.Count();
            }
            return gpsItems;
        }
    }
}