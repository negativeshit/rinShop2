using System;
using Dapper;
using System.Data;
using SV19T1021242.DomainModels;

namespace SV19T1021242.DataLayers.SQLServer
{
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        public ProductDAL(string connectionString) : base(connectionString)
        {
        }
        
        public int Add(Product data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"
                                    insert into Products(ProductName,ProductDescription,SupplierId,CategoryId,Unit,Price,Photo,IsSelling)
                                    values(@ProductName,@ProductDescription,@SupplierId,@CategoryId,@Unit,@Price,@Photo,@IsSelling);
                                    select @@identity;
                                ";
                var parameters = new
                {
                    ProductName = data.ProductName ?? "",
                    ProductDescription = data.ProductDescription ?? "",
                    SupplierId = data.SupplierID,
                    CategoryId = data.CategoryId,
                    Unit = data.Unit ?? "",
                    Price = data.Price,
                    Photo = data.Photo,
                    IsSelling = data.IsSelling


                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return id;
        }

        public long AddAttribute(ProductAttribute data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"
                                insert into ProductAttributes(ProductID,AttributeName,AttributeValue,DisplayOrder)
                                    values(@ProductID,@AttributeName,@AttributeValue,@DisplayOrder);
                                    select @@identity;
                                ";
                var parameters = new
                {
                    ProductID = data.ProductID,
                    AttributeName = data.AttributeName ?? "",
                    AttributeValue = data.AttributeValue ?? "",
                    DisplayOrder = data.DisplayOrder,
                    

                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return id;
        }

        public long AddPhoto(ProductPhoto data)
        {
            int id = 0;
            using (var connection = OpenConnection())
            {
                var sql = @"
                                    insert into ProductPhotos(ProductID,Photo,Description,DisplayOrder,IsHidden)
                                    values(@ProductID,@Photo,@Description,@DisplayOrder,@IsHidden);
                                    select @@identity;
                                ";
                var parameters = new
                {
                    ProductID = data.ProductID,
                    Photo = data.Photo ?? "",
                    DisplayOrder = data.DisplayOrder,
                    Description = data.Description ?? "",
                    IsHidden = data.IsHidden,



                };
                id = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: CommandType.Text);
                connection.Close();
            }
            return id;
        }

        public int Count(string searchValue = "", int categoryID = 0, int supplierID = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            int count = 0;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"select  count(*)
                            from    Products
                            where   (@SearchValue = N'' or ProductName like @SearchValue)
                                and (@CategoryID = 0 or CategoryID = @CategoryID)
                                and (@SupplierID = 0 or SupplierId = @SupplierID)
                                and (Price >= @MinPrice)
                                and (@MaxPrice <= 0 or Price <= @MaxPrice)";
                var parameters = new
                {
                    searchValue = searchValue,
                    CategoryID = categoryID,
                    SupplierID = supplierID,
                    MinPrice = minPrice,
                    MaxPrice = maxPrice
                };
                count = connection.ExecuteScalar<int>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return count;
        }

        public bool Delete(int ProductId)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from Products where ProductID = @ProductId";
                var parameters = new
                {
                    ProductId = ProductId
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public bool DeleteAttribute(long AttributeId)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from ProductAttributes where AttributeID = @AttributeId";
                var parameters = new
                {
                    AttributeId = AttributeId
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }
            public bool DeletePhoto(long PhotoId)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"delete from ProductPhotos where PhotoID = @PhotoId";
                var parameters = new
                {
                    PhotoId = PhotoId
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public Product? Get(int ProductId)
        {
            Product? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from Products where ProductID = @ProductId";
                var parameters = new
                {
                    ProductId = ProductId
                };
                data = connection.QueryFirstOrDefault<Product>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public ProductAttribute? GetAttribute(long AttributeId)
        {
            ProductAttribute? data = null;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from ProductAttributes where AttributeID = @AttributeId";
                var parameters = new
                {
                    AttributeID = AttributeId
                };
                data = connection.QueryFirstOrDefault<ProductAttribute>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public ProductPhoto? GetPhoto(long PhotoID)
        {
            ProductPhoto? data = null;

            using (var connection = OpenConnection())
            {
                var sql = @"select * from ProductPhotos where PhotoID = @PhotoID";

                var parameters = new
                {
                    PhotoID = PhotoID
                };

                data = connection.QueryFirstOrDefault<ProductPhoto>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return data;
        }

        public bool IsUsed(int ProductId)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {
                var sql = @"if exists(select * from OrderDetails where ProductId = @ProductId)
                                select 1
                            else 
                                select 0";
                var parameters = new
                {
                    ProductId = ProductId
                };
                result = connection.ExecuteScalar<bool>(sql: sql, param: parameters, commandType: System.Data.CommandType.Text);
                connection.Close();
            }
            return result;
        }
        
        public IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "", int CategoryId = 0, int supplierId = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            List<Product> data;
            if (!string.IsNullOrEmpty(searchValue))
                searchValue = "%" + searchValue + "%";
            using (var connection = OpenConnection())
            {
                var sql = @"with cte as
(
    select  *,
            row_number() over(order by ProductName) as RowNumber
    from    Products
    where   (@SearchValue = N'' or ProductName like @SearchValue)
        and (@CategoryId = 0 or CategoryId = @CategoryId)
        and (@SupplierID = 0 or SupplierId = @SupplierID)
        and (Price >= @MinPrice)
        and (@MaxPrice <= 0 or Price <= @MaxPrice)
)
select * from cte
where   (@PageSize = 0)
    or (RowNumber between (@Page - 1)*@PageSize + 1 and @Page * @PageSize)";
                var parameters = new
                {
                    page = page,
                    pageSize = pageSize,
                    searchValue = searchValue,
                    CategoryId = CategoryId,
                    SupplierId = supplierId,
                    minPrice = minPrice,
                    maxPrice = maxPrice
                };
                data = (connection.Query<Product>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                connection.Close();
            }

            return data;
        }

        public IList<ProductAttribute> ListAttributes(int ProductId)
        {
            List<ProductAttribute> data;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from ProductAttributes where ProductID = @ProductId order by DisplayOrder asc";
                var parameters = new
                {
                    ProductID = ProductId,
                };
                data = (connection.Query<ProductAttribute>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                connection.Close();
            }
            return data;
        }

        public IList<ProductPhoto> ListPhotos(int ProductId)
        {
            List<ProductPhoto> data;
            using (var connection = OpenConnection())
            {
                var sql = @"select * from ProductPhotos where ProductId = @ProductId";
                var parameters = new
                {
                    ProductID = ProductId,
                };
                data = (connection.Query<ProductPhoto>(sql: sql, param: parameters, commandType: CommandType.Text)).ToList();
                connection.Close();
            }
            return data;
        }

        public bool Update(Product data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {

                var sql = @"
                                    update Products 
                                   set ProductName = @ProductName,
                                       ProductDescription = @ProductDescription,
                                       SupplierId = @SupplierId,
                                       CategoryId = @CategoryId,
                                       Unit = @Unit,
                                       Price = @Price,
                                       Photo = @Photo,
                                       IsSelling = @IsSelling
                                    where ProductId = @ProductId
                                ";
                var parameters = new
                {
                    ProductID = data.ProductID,
                    ProductName = data.ProductName ?? "",
                    ProductDescription = data.ProductDescription ?? "",
                    SupplierId = data.SupplierID,
                    CategoryId = data.CategoryId,
                    Unit = data.Unit ?? "",
                    Price = data.Price,
                    Photo = data.Photo,
                    IsSelling = data.IsSelling
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public bool UpdateAttribute(ProductAttribute data)
        {
            bool result = false;
            using (var connection = OpenConnection())
            {

                var sql = @" Update ProductAttributes
set 
    ProductID = @ProductID,
    AttributeName = @AttributeName,
    AttributeValue = @AttributeValue,
    DisplayOrder = @DisplayOrder
Where AttributeID  = @AttributeID";
                var parameters = new
                {
                    AttributeID = data.AttributeID,
                    ProductID = data.ProductID,
                    AttributeName = data.AttributeName ?? "",
                    AttributeValue = data.AttributeValue ?? "",
                    DisplayOrder = data.DisplayOrder,
                };
                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }
            return result;
        }

        public bool UpdatePhoto(ProductPhoto data)
        {
            bool result = false;

            using (var connection = OpenConnection())
            {
                var sql = @"UPDATE ProductPhotos
                            SET Photo = @Photo,
                                Description = @Description,
                                DisplayOrder = @DisplayOrder,
                                IsHidden = @IsHidden
                            WHERE PhotoID = @PhotoID";

                var parameters = new
                {
                    PhotoID = data.PhotoID,
                    ProductID = data.ProductID,
                    Photo = data.Photo,
                    Description = data.Description,
                    DisplayOrder = data.DisplayOrder,
                    IsHidden = data.IsHidden
                };

                result = connection.Execute(sql: sql, param: parameters, commandType: System.Data.CommandType.Text) > 0;
                connection.Close();
            }

            return result;
        }
    }
}

