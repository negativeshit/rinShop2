using System;
using System.Globalization;

namespace SV19T1021242.Web
{
	public static class Converter
	{
        public static DateTime? StringToDateTime(this string s, string formats = "d/M/yyyy;d-M-yyyy;d.M.yyyy")
        {
            try
            {
                return DateTime.ParseExact(s, formats.Split(";"), CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }
    }
}

