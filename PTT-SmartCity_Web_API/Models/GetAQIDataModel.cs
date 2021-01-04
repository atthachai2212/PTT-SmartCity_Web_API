using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GetAQIDataModel
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Zone { get; set; }
        public GetAQIData O3 { get; set; }
        public GetAQIData PM2_5 { get; set; }
        public GetAQIData PM10 { get; set; }
        public GetAQIData CO { get; set; }
        public GetAQIData SO2 { get; set; }
        public GetAQIData NO2 { get; set; }
        public GetAQIData AQI { get; set; }
    }

    public class GetAQIData
    {
        public float Value { get; set; }
        public int AQI { get; set; }
        public string AQI_Level { get; set; }
        public string AQI_Color { get; set; }
    }

    //public class AQICategory
    //{
    //    string _Good;
    //    string _Moderate;
    //    string _Unhealthy_for_ensitive_Groups;
    //    string _Unhealthy;
    //    string _Very_Unhealthy;
    //    string _Hazardous;
    //    public string Good
    //    {
    //        get { return _Good; }
    //        set { _Good = value; }
    //    }
    //    public string Moderate
    //    {
    //        get { return _Moderate; }
    //        set { _Moderate = value; }
    //    }
    //    public string Unhealthy_for_ensitive_Groups
    //    {
    //        get { return _Unhealthy_for_ensitive_Groups; }
    //        set { _Unhealthy_for_ensitive_Groups = value; }
    //    }
    //    public string Unhealthy
    //    {
    //        get { return _Unhealthy; }
    //        set { _Unhealthy = value; }
    //    }
    //    public string Very_Unhealthy
    //    {
    //        get { return _Very_Unhealthy; }
    //        set { _Very_Unhealthy = value; }
    //    }

    //    public string Hazardous
    //    {
    //        get { return _Hazardous; }
    //        set { _Hazardous = value; }
    //    }
    //    public AQICategory()
    //    {
    //        _Good = "Good";
    //        _Moderate = "Moderate";
    //        _Unhealthy_for_ensitive_Groups = "Unhealthy for Sensitive Groups";
    //        _Very_Unhealthy = "Very Unhealthy";
    //        _Hazardous = "Very Unhealthy";
    //    }
    //}
    public class AQICategory
    {
        private AQICategory(string value) { Value = value; }

        public string Value { get; set; }

        public static AQICategory Good { get { return new AQICategory("Good"); } }
        public static AQICategory Moderate { get { return new AQICategory("Moderate"); } }
        public static AQICategory Unhealthy_for_ensitive_Groups { get { return new AQICategory("Unhealthy for Sensitive Groups"); } }
        public static AQICategory Unhealthy { get { return new AQICategory("Unhealthy"); } }
        public static AQICategory Very_Unhealthy { get { return new AQICategory("Very Unhealthy"); } }
        public static AQICategory Hazardous { get { return new AQICategory("Very Unhealthy"); } }
    }
}