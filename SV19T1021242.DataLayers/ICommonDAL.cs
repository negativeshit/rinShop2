using System;
namespace SV19T1021242.DataLayers
{
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// Tìm kiếm và lấy dữ liệu dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng hiển thị trên mỗi trang (bằng 0 nếu không phân trang dữ liệu)</param>
        /// <param name="seacrchValue">Giá trị cần tìm kiếm (chuỗi rỗng nếu lấy toàn bộ dữ liệu)</param>
        /// <returns></returns>
        IList<T> List(int page = 1, int pageSize = 0, string searchValue = "");
        /// <summary>
        /// Đếm số dòng dữ liệu tìm được
        /// </summary>
        /// <param name="seacrchValue">Giá trị cần tìm kiếm (chuỗi rỗng nếu lấy toàn bộ dữ liệu)</param>
        /// <returns></returns>
        int Count(String searchValue = "");
        /// <summary>
        /// Bổ sung dữ liệu vào cơ sở dữ liệu. Hmaf trả về ID của dữ liệu được bổ sung
        /// (trả về giá trị 0 nếu việc bổ sung không thành công)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(T data);
        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(T data);
        /// <summary>
        /// Xóa dữ liệu dựa trên id
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// Lấy một bản ghi dựa vào id (trả về null nếu dữ liệu không tồn tại)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T? Get(int id);
        /// <summary>
        /// Kiểm tra xem bản ghi dữ liệu có mã id hiện đang có được sử dụng bởi các dữ liệu khác hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsUsed(int id);
    }
}

