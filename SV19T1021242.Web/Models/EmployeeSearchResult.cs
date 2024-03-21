using System;
using SV19T1021242.DomainModels;

namespace SV19T1021242.Web.Models
{
	public class EmployeeSearchResult : BasePaginationResult
	{
		public List<Employee> Data { get; set; }
		
	}
}

