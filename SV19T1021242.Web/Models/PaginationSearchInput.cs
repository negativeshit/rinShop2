using System;
namespace SV19T1021242.Web.Models
{
	/// <summary>
	/// Đầu vào tìm kiếm nhập dữ liệu phân trang
	/// </summary>
	public class PaginationSearchInput
	{
		public int Page { get; set; }
		public int PageSize { get; set; } = 0;
		public string SearchValue { get; set; } = "";
        public int categoryID { get; set; }
        public int supplierID { get; set; }
    }
}

