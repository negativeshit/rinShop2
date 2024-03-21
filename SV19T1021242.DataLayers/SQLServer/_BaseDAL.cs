using System;
using Microsoft.Data.SqlClient;

namespace SV19T1021242.DataLayers.SQLServer
{
	public abstract class _BaseDAL
	{
		protected string _connectionString = "";
		public _BaseDAL(string connectionString)
		{
			_connectionString = connectionString;
		}
        protected SqlConnection OpenConnection()
		{
			SqlConnection connection = new SqlConnection();
			connection.ConnectionString = _connectionString;
			connection.Open();
			return connection;
		}
	}
}

