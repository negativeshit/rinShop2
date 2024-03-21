using System;
using SV19T1021242.DomainModels;

namespace SV19T1021242.Web.Models
{
    public class SupplierSearchResult : BasePaginationResult
    {
        public List<Supplier>? Data { get; set; }
    }
}

