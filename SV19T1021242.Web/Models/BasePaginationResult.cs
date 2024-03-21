using System;
namespace SV19T1021242.Web.Models
{
	/// <summary>
	/// Biểu diễn lớp cha kết quả tìm kiếm phân trang
	/// </summary>
	public abstract class BasePaginationResult
	{
		public int Page { get; set; }
		public int PageSize { get; set; }
		public string SearchValue { get; set; } = "";
		public  int RowCount { get; set; }
		public  int PageCount
		{
			get
			{
				if (PageSize == 0)
					return 1;
				int c = RowCount / PageSize;
				if (RowCount % PageSize > 1)
					c += 1;
				return c;
			}
		}
	}
}

