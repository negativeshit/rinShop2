using System;
namespace SV19T1021242.BusinessLayers
{
	public static class Configuration
	{
		/// <summary>
		/// Chuoi ket thong so ket noi CSDL
		/// </summary>
		public static string ConnectionString { get; private set; } = "";
		/// <summary>
		/// Khoi tao cau hinh businesslayer
		/// Khoi dong truoc khi ung dung chay
		/// </summary>
		/// <param name="connectionString"></param>
		public static void Initialize(string connectionString)
		{
			Configuration.ConnectionString = connectionString;
		}
	}
}

