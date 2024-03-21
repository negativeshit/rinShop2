using System;
using SV19T1021242.DomainModels;

namespace SV19T1021242.DataLayers
{
    public interface IProductDAL
    {
       
       /// <summary>
       /// Tìm kiếm và lấy danh sách đơn hàng dưới dạng phân trang
       /// </summary>
       /// <param name="page">Trang cần hiển thị</param>
       /// <param name="pageSize">Số dòng mỗi trang ( 0 nếu phân trang )</param>
       /// <param name="searchValue">Tên mặt hàng cần tìm ( chuỗi rỗng nếu không tìm kiếm )</param>
       /// <param name="categoryId">Mã loại hàng cần tìm ( 0 nếu không tìm theo loại hàng)</param>
       /// <param name="supplierId">Mã nhà cung cấp (0 nếu không tìm ra nhà cung cấp )</param>
       /// <param name="minPrice">Mức giá nhỏ nhất trong khoảng giá cần tìm</param>
       /// <param name="maxPrice">Mức giá lớn nhất trong khoảng giá cần tìm</param>
       /// <returns></returns>
        IList<Product> List(int page = 1, int pageSize = 0, string searchValue = ""
            ,int categoryId = 0, int supplierId = 0, decimal minPrice = 0 , decimal maxPrice = 0);
       /// <summary>
       /// Đếm số lượng mặt hàng kiếm được
       /// </summary>
       /// <param name="searchValue"></param>
       /// <param name="categoryId"></param>
       /// <param name="supplierId"></param>
       /// <param name="minPrice"></param>
       /// <param name="maxPrice"></param>
       /// <returns></returns>
        int Count(String searchValue = "", int categoryId = 0, int supplierId = 0, decimal minPrice = 0, decimal maxPrice = 0);
      /// <summary>
      /// Bổ sung mặt hàng mới
      /// </summary>
      /// <param name="data"></param>
      /// <returns></returns>
        int Add(Product data);
       /// <summary>
       /// Cập nhật mặt hàng
       /// </summary>
       /// <param name="data"></param>
       /// <returns></returns>
        bool Update(Product data);
       /// <summary>
       /// Xoá mặt hàng
       /// </summary>
       /// <param name="ProductId"></param>
       /// <returns></returns>
        bool Delete(int ProductId);
        /// <summary>
        /// Lấy thông tin mặt hàng theo mã hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Product? Get(int ProductId);
       /// <summary>
       /// Kiểm tra mặt hàng có liên quan hay không?
       /// </summary>
       /// <param name="ProductId"></param>
       /// <returns></returns>
        bool IsUsed(int ProductId);
        /// <summary>
        /// Lấy danh sách ảnh của mặt hàng
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        IList<ProductPhoto> ListPhotos(int ProductId);
        /// <summary>
        /// Lấy thông tin ảnh dựa theo ID
        /// </summary>
        /// <param name="PhotoId"></param>
        /// <returns></returns>
        ProductPhoto? GetPhoto(long PhotoId);
        /// <summary>
        /// Bổ sung 1 ảnh cho mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddPhoto(ProductPhoto data);
        /// <summary>
        /// Cập nhật ảnh cho mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdatePhoto(ProductPhoto data);
        /// <summary>
        /// Xoá ảnh của mặt hàng
        /// </summary>
        /// <param name="PhotoId"></param>
        /// <returns></returns>
        bool DeletePhoto(long PhotoId);
        /// <summary>
        /// lấy danh sách thuộc tính măt hàng, sắp xếp theo display order
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        IList<ProductAttribute> ListAttributes(int ProductId);
        /// <summary>
        /// Lấy thông tin thuộc tính theo mã hàng
        /// </summary>
        /// <param name="AttributeId"></param>
        /// <returns></returns>
        ProductAttribute? GetAttribute(long AttributeId);
        /// <summary>
        /// Bổ sung thuộc tính cho mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        long AddAttribute(ProductAttribute data);
        /// <summary>
        /// Cập nhật thuộc tính cho mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool UpdateAttribute(ProductAttribute data);
        /// <summary>
        /// Xoá thuộc tính
        /// </summary>
        /// <param name="AttributeId"></param>
        /// <returns></returns>
        bool DeleteAttribute(long AttributeId);

    }
}

