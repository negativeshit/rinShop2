using System;
using Microsoft.Data.SqlClient;
using Dapper;
using SV19T1021242.DomainModels;
using System.Data;
using System.Net;
using System.Numerics;
using SV19T1021242.DataLayers;
using SV19T1021242.DataLayers.SQLServer;

namespace SV19T1021242.DataLayers.SQLServer
{
    public class ShipperDAL : _BaseDAL, ICommonDAL<Shipper>
    {
        public ShipperDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Shipper data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from Shippers where ShipperId = @ShipperId)
                                select -1
                            else
                                begin
                                    insert into Shippers(ShipperName,Phone)
                                    values(@ShipperName,@Phone);
                                    select @@identity;
                                end";
                var parameters = new
                {
                    ShipperId = data.ShipperId,
                    ShipperName = data.ShipperName ?? "",
                    Phone = data.Phone

                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return id;
        }

        public int Count(string searchValue = "")
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
            { searchValue = "%" + searchValue + "%"; }
            using (var connection = OpenConnection())
            {
                var sql = @"select count(*) from Shippers 
                            where (@searchValue = N'') or (ShipperName like @searchValue)";
                var parameters = new { searchValue = searchValue };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public bool Delete(int Id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from Shippers where ShipperId = @ShipperId";
                var parameters = new
                {
                    ShipperId = Id
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public Shipper? Get(int Id)
        {
            Shipper? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from Shippers where ShipperId = @ShipperId";
                var parameters = new
                {
                    ShipperId = Id
                };
                data = connection.QueryFirstOrDefault<Shipper>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public bool IsUsed(int Id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from Orders where ShipperId = @ShipperId)
                                select 1
                            else 
                                select 0";
                var parameters = new
                {
                    ShipperId = Id
                };
                result = connection.ExecuteScalar<bool>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return result;
        }

        public IList<Shipper> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Shipper> data;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
	                            select	*, row_number() over (order by ShipperName) as RowNumber
	                            from	Shippers 
	                            where	(@searchValue = N'') or (ShipperName like @searchValue)
                            )
                            select * from cte
                            where  (@pageSize = 0) 
	                            or (RowNumber between (@page - 1) * @pageSize + 1 and @page * @pageSize)
                            order by RowNumber";
                var parameters = new
                {
                    page = page,
                    pageSize = pageSize,
                    searchValue = searchValue ?? ""
                };
                data = (connection.Query<Shipper>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                connection.Close();
            }

            return data;
        }

        public bool Update(Shipper data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                /// tồn tại một Shipperid = @Shipperid 
                var sql = @"
                            update Shippers 
                            set ShipperName = @shipperName,
                                Phone = @phone
                            where ShipperId = @ShipperId
                             ";
                var parameters = new
                {
                    ShipperId = data.ShipperId,
                    ShipperName = data.ShipperName ?? "",
                    Phone = data.Phone ?? "",

                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
    }
}

