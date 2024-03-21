using System;
using SV19T1021242.DomainModels;

namespace SV19T1021242.Web.Models
{
    public class CategorySearchResult : BasePaginationResult
    {
        public List<Category>? Data { get; set; }
    }
}

