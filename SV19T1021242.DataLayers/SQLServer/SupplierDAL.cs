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
    public class SupplierDAL : _BaseDAL, ICommonDAL<Supplier>
    {
        public SupplierDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Supplier data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"insert into Suppliers(SupplierName,ContactName,Province,Address,Phone,Email)
                            values(@SupplierName,@ContactName,@Province,@Address,@Phone,@Email);
                            select @@identity;";
                var parameters = new
                {
                    SupplierId = data.SupplierId,
                    SupplierName = data.SupplierName ?? "",
                    ContactName = data.ContactName ?? "",
                    Province = data.Province ?? "",
                    Address = data.Address ?? "",
                    Phone = data.Phone ?? "",
                    Email = data.Email

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
                var sql = @"select count(*) from Suppliers 
                            where (@searchValue = N'') or (SupplierName like @searchValue)";
                var parameters = new
                {
                    searchValue = searchValue ?? ""
                };
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
                var sql = @"delete from Suppliers where SupplierId = @SupplierId";
                var parameters = new
                {
                    SupplierId = Id
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public Supplier? Get(int Id)
        {
            Supplier? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from Suppliers where SupplierId = @SupplierId";
                var parameters = new
                {
                    SupplierId = Id
                };
                data = connection.QueryFirstOrDefault<Supplier>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// Chưa làm
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool IsUsed(int Id)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from Products where SupplierId = @supplierId)
                                select 1
                            else 
                                select 0";
                var parameters = new
                {
                    SupplierId = Id
                };
                result = connection.ExecuteScalar<bool>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return result;
        }

        public IList<Supplier> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Supplier> data;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
                            (
	                            select	*, row_number() over (order by SupplierName) as RowNumber
	                            from	Suppliers 
	                            where	(@searchValue = N'') or (SupplierName like @searchValue)
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
                data = (connection.Query<Supplier>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                connection.Close();
            }

            return data;
        }

        public bool Update(Supplier data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {

                var sql = @"update Suppliers 
                            set SupplierName = @SupplierName,
                                ContactName = @contactName,
                                Province = @province,
                                Address = @address,
                                Phone = @phone,
                                Email = @email
                             where SupplierId = @SupplierId";
                var parameters = new
                {
                    SupplierId = data.SupplierId,
                    SupplierName = data.SupplierName ?? "",
                    ContactName = data.ContactName ?? "",
                    Province = data.Province ?? "",
                    Address = data.Address ?? "",
                    Phone = data.Phone ?? "",
                    Email = data.Email
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
    }
}
