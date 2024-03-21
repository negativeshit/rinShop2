using System;
using Azure;
using SV19T1021242.DataLayers;
using SV19T1021242.DataLayers.SQLServer;
using SV19T1021242.DomainModels;

namespace SV19T1021242.BusinessLayers
{
    public class ProductDataService
    {
        private static readonly IProductDAL ProductDB;
        static ProductDataService()
        {
            ProductDB = new ProductDAL(Configuration.ConnectionString);
        }
        public static List<Product> ListProducts(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "", int categoryId = 0, int supplierId = 0, decimal minPrice = 0, decimal maxPrice = 0)
        {
            rowCount = ProductDB.Count(searchValue, categoryId, supplierId, minPrice, maxPrice);
            return ProductDB.List(page, pageSize, searchValue,categoryId,supplierId,minPrice,maxPrice).ToList();
        }
        public static Product? GetProduct(int ProductId)
        {
            return ProductDB.Get(ProductId);
        }
        public static int AddProduct(Product product)
        {
            return ProductDB.Add(product);
        }
        public static bool UpdateProduct(Product product)
        {
            return ProductDB.Update(product);
        }
        public static bool DeleteProduct(int productID)
        {
            if (ProductDB.IsUsed(productID))
                return false;
            return ProductDB.Delete(productID);
        }
        public static bool IsUsedProduct(int productID)
        {
            return ProductDB.IsUsed(productID);
        }
        public static List<ProductPhoto> ListPhotos(int productID)
        {
            return ProductDB.ListPhotos(productID).ToList();
        }
        public static ProductPhoto? GetPhoto(long photoID)
        {
            return ProductDB.GetPhoto(photoID);
        }
        public static long AddPhoto(ProductPhoto data)
        {
            return ProductDB.AddPhoto(data);
        }
        public static bool UpdatePhoto(ProductPhoto data)
        {
            return ProductDB.UpdatePhoto(data);
        }
        public static bool DeletePhoto(long photoID)
        {
            return ProductDB.DeletePhoto(photoID);
        }
        public static List<ProductAttribute> ListAttributes(int productID)
        {
            return ProductDB.ListAttributes(productID).ToList();
        }
        public static ProductAttribute? GetAttribute(int attributeID)
        {
            return ProductDB.GetAttribute(attributeID);
        }
        public static long AddAttribute(ProductAttribute data)
        {
            return ProductDB.AddAttribute(data);
        }
        public static bool UpdateAttribute(ProductAttribute data)
        {
            return ProductDB.UpdateAttribute(data);
        }
        public static bool DeleteAttribute(long attributeID)
        {
            return ProductDB.DeleteAttribute(attributeID);
        }
    }
}

