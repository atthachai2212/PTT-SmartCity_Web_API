using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public static double KtoCelsius(double CTempIn)
        {
            double KCel = CTempIn - 273.15;
            return KCel;
        }

    }
}