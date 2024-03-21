using System;
using SV19T1021242.DomainModels;

namespace SV19T1021242.Web.Models
{
    /// <summary>
    /// biểu diễn dữ liệu kết quả tìm kiếm đơn hàng
    /// </summary>
    public class OrderSearchResult : BasePaginationResult
    {
        public int Status { get; set; } = 0;
        public string TimeRange { get; set; } = "";
        public List<Order> Data { get; set; } = new List<Order>();
    }
}

