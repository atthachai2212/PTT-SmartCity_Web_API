using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public static class HelperService
    {
        public static string ToHexString(this byte[] bts)
        {
            return BitConverter.ToString(bts).Replace("-", "");
        }

        public static byte[] StringToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        public static string Base64String(this byte[] bts)
        {
            return Convert.ToBase64String(bts);
        }

        public static byte[] ToByteFromHex(this string hex)
        {
            return Convert.FromBase64String(hex);
        }

        public static int ByteToInt16(this byte[] value)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(value);

            return BitConverter.ToInt16(value, 0);
        }

        public static IEnumerable<string> SplitString(string str, int Size)
        {
            return Enumerable.Range(0, str.Length / Size)
                .Select(i => str.Substring(i * Size, Size));
        }

        public static float HexToFloatingPoint(string hex)
        {
            uint data = uint.Parse(hex, NumberStyles.AllowHexSpecifier);
            byte[] bytes = BitConverter.GetBytes(data);
            if (BitConverter.IsLittleEndian)
            {
                bytes = bytes.Reverse().ToArray();
            }
            return BitConverter.ToSingle(bytes, 0);
        }

        public static int HexToInt32(string strHex)
        {
            if (strHex.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                strHex = strHex.Substring(2);
            }
            return Int32.Parse(strHex, NumberStyles.HexNumber);
        }

        public static short HexToInt16(this string strHex)
        {
            if (strHex.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                strHex = strHex.Substring(2);
            }
            return Int16.Parse(strHex, NumberStyles.HexNumber);
        }

        public static string HexToBinary(string hexvalue)
        {
            // Convert.ToUInt32 this is an unsigned int
            // so no negative numbers but it gives you one more bit
            // it much of a muchness 
            // Uint MAX is 4,294,967,295 and MIN is 0
            // this padds to 4 bits so 0x5 = "0101"
            return String.Join(String.Empty, hexvalue.Select(c => Convert.ToString(Convert.ToUInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
        }

        public static double ConvertDegreesMinutesMToDecimalDegrees(double degrees, double minutesm)
        {
            //var d = minutesm / 60;
            //var decimalDegrees = degrees + d;
            return degrees + (minutesm / 60);
        }

        public static double KtoCelsius(double CTempIn)
        {
            double KCel = CTempIn - 273.15;
            return KCel;
        }

        public static int Pollution_PM25Cal(float avg)
        {
            if (avg >= 0)
            {
                float Ip = 0;
                int Ilow = 0, Ihigh = 0;
                float Clow = 0, Chigh = 0;
                float Cp = 0;
                var PollutionValue_Cal = AQIBreakpointTable("PM25", avg);

                Ilow = Convert.ToInt32(PollutionValue_Cal["Ilow"]);
                Ihigh = Convert.ToInt32(PollutionValue_Cal["Ihigh"]);
                Clow = Convert.ToSingle(PollutionValue_Cal["Clow"]);
                Chigh = Convert.ToSingle(PollutionValue_Cal["Chigh"]);
                Cp = Convert.ToSingle(PollutionValue_Cal["Cp"]);
                Ip = (Ihigh - Ilow) / (Chigh - Clow) * (Cp - Clow) + Ilow;
                return Convert.ToInt32(Ip);
            }
            else
            {
                return Convert.ToInt32(avg);
            }

        }

        public static int Pollution_PM10Cal(float avg)
        {
            if (avg >= 0) 
            {
                float Ip = 0;
                int Ilow = 0, Ihigh = 0;
                float Clow = 0, Chigh = 0;
                float Cp = 0;
                var PollutionValue_Cal = AQIBreakpointTable("PM10", avg);

                Ilow = Convert.ToInt32(PollutionValue_Cal["Ilow"]);
                Ihigh = Convert.ToInt32(PollutionValue_Cal["Ihigh"]);
                Clow = Convert.ToSingle(PollutionValue_Cal["Clow"]);
                Chigh = Convert.ToSingle(PollutionValue_Cal["Chigh"]);
                Cp = Convert.ToSingle(PollutionValue_Cal["Cp"]);
                Ip = (Ihigh - Ilow) / (Chigh - Clow) * (Cp - Clow) + Ilow;
                return Convert.ToInt32(Ip);
            } 
            else
            {
                return Convert.ToInt32(avg);
            }

        }

        public static int Pollution_O3Cal(float avg)
        {
            if(avg >= 0)
            {
                float Ip = 0;
                int Ilow = 0, Ihigh = 0;
                float Clow = 0, Chigh = 0;
                float Cp = 0;
                var PollutionValue_Cal = AQIBreakpointTable("O3", avg);

                Ilow = Convert.ToInt32(PollutionValue_Cal["Ilow"]);
                Ihigh = Convert.ToInt32(PollutionValue_Cal["Ihigh"]);
                Clow = Convert.ToSingle(PollutionValue_Cal["Clow"]);
                Chigh = Convert.ToSingle(PollutionValue_Cal["Chigh"]);
                Cp = Convert.ToSingle(PollutionValue_Cal["Cp"]);
                Ip = (Ihigh - Ilow) / (Chigh - Clow) * (Cp - Clow) + Ilow;
                return Convert.ToInt32(Ip);
            }
            else
            {
                return Convert.ToInt32(avg);
            }
        }

        public static Dictionary<string, float> AQIBreakpointTable(string Checkname, float Checkpoint)
        {
            var dicAQI = new Dictionary<string, float>();
            int Ilow = 0, Ihigh = 0;
            float Clow = 0, Chigh = 0;
            float Cp = Checkpoint;
            switch (Checkname)
            {
                case "PM25":
                    if (Checkpoint >= 0 && Checkpoint <= 12)
                    {
                        Ilow = 0; Ihigh = 50;
                        Clow = 0; Chigh = 12;
                    }
                    else if (Checkpoint >= 12.1 && Checkpoint <= 35.4)
                    {
                        Ilow = 51; Ihigh = 100;
                        Clow = 12.1f; Chigh = 35.4f;
                    }
                    else if (Checkpoint >= 35.5 && Checkpoint <= 55.4)
                    {
                        Ilow = 101; Ihigh = 150;
                        Clow = 35.5f; Chigh = 55.4f;
                    }
                    else if (Checkpoint >= 55.5 && Checkpoint <= 150.4)
                    {
                        Ilow = 151; Ihigh = 200;
                        Clow = 55.5f; Chigh = 150.4f;
                    }
                    else if (Checkpoint >= 150.5 && Checkpoint <= 250.4)
                    {
                        Ilow = 201; Ihigh = 300;
                        Clow = 150.5f; Chigh = 250.4f;
                    }
                    else if (Checkpoint >= 250.5 && Checkpoint <= 350.4)
                    {
                        Ilow = 301; Ihigh = 400;
                        Clow = 250.5f; Chigh = 500.4f;
                    }
                    else if (Checkpoint >= 350.5 && Checkpoint <= 500.4)
                    {
                        Ilow = 401; Ihigh = 500;
                        Clow = 250.5f; Chigh = 500.4f;
                    }
                    break;
                case "PM10":
                    if (Checkpoint >= 0 && Checkpoint <= 54)
                    {
                        Ilow = 0; Ihigh = 50;
                        Clow = 0; Chigh = 54;
                    }
                    else if (Checkpoint >= 55 && Checkpoint <= 154)
                    {
                        Ilow = 51; Ihigh = 100;
                        Clow = 55; Chigh = 154;
                    }
                    else if (Checkpoint >= 155 && Checkpoint <= 254)
                    {
                        Ilow = 101; Ihigh = 150;
                        Clow = 155; Chigh = 254;
                    }
                    else if (Checkpoint >= 255 && Checkpoint <= 354)
                    {
                        Ilow = 151; Ihigh = 200;
                        Clow = 255; Chigh = 354;
                    }
                    else if (Checkpoint >= 355 && Checkpoint <= 424)
                    {
                        Ilow = 201; Ihigh = 300;
                        Clow = 355; Chigh = 424;
                    }
                    else if (Checkpoint >= 425 && Checkpoint < 504)
                    {
                        Ilow = 301; Ihigh = 400;
                        Clow = 425; Chigh = 504;
                    }
                    else if (Checkpoint >= 505 && Checkpoint < 604)
                    {
                        Ilow = 301; Ihigh = 400;
                        Clow = 425; Chigh = 504;
                    }
                    break;
                case "O3":
                    if (Checkpoint >= 0 && Checkpoint <= 54)
                    {
                        Ilow = 0; Ihigh = 50;
                        Clow = 0; Chigh = 54;
                    }
                    else if (Checkpoint >= 55 && Checkpoint <= 70)
                    {
                        Ilow = 51; Ihigh = 100;
                        Clow = 55; Chigh = 70;
                    }
                    else if (Checkpoint >= 71 && Checkpoint <= 85)
                    {
                        Ilow = 101; Ihigh = 150;
                        Clow = 71; Chigh = 85;
                    }
                    else if (Checkpoint >= 86 && Checkpoint <= 105)
                    {
                        Ilow = 151; Ihigh = 200;
                        Clow = 86; Chigh = 105;
                    }
                    else if (Checkpoint >= 106 && Checkpoint <= 200)
                    {
                        Ilow = 201; Ihigh = 300;
                        Clow = 106; Chigh = 200;
                    }
                    break;
                default:
                    break;
            }
            dicAQI.Add("Ilow", Ilow);
            dicAQI.Add("Ihigh", Ihigh);
            dicAQI.Add("Clow", Clow);
            dicAQI.Add("Chigh", Chigh);
            dicAQI.Add("Cp", Cp);
            return dicAQI;
        }

        public static Dictionary<string, string> AQIPollutionLevel_Cal(int AQI)
        {
            string Level = string.Empty;
            string Color = string.Empty;
            var dicAQIPollutionLevel = new Dictionary<string, string>();
            if (AQI >= 0 && AQI <= 50)
            {
                Level = "Good";
                Color = "Green";
            }
            else if (AQI >= 51 && AQI <= 100)
            {
                Level = "Moderate";
                Color = "Yellow";
            }
            else if (AQI >= 101 && AQI <= 150)
            {
                Level = "Unhealthy for Sensitive Groups";
                Color = "Orange";
            }
            else if (AQI >= 151 && AQI <= 200)
            {
                Level = "Unhealthy";
                Color = "Red";
            }
            else if (AQI >= 201 && AQI <= 300)
            {
                Level = "Very Unhealthy";
                Color = "Puple";
            }
            else if (AQI > 300)
            {
                Level = "Hazardous";
                Color = "Maroon";
            }
            dicAQIPollutionLevel.Add("Level", Level);
            dicAQIPollutionLevel.Add("Color", Color);

            return dicAQIPollutionLevel;
        }

    }
}