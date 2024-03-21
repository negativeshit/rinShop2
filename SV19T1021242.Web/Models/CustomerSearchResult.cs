using System;
using SV19T1021242.DomainModels;

namespace SV19T1021242.Web.Models
{
    /// <summary>
    ///	Kết quả tìm kiếm và lấy danh sách khách hàng
    /// </summary>
    public class CustomerSearchResult : BasePaginationResult
    {
        public List<Customer> Data { get; set; }
    }
}

