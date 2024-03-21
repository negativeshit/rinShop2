using System;
using SV19T1021242.DomainModels;

namespace SV19T1021242.Web.Models
{
    public class ProductSearchResult : BasePaginationResult
    {
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public decimal minPrice { get; set; }
        public decimal maxPrice { get; set; }
        public List<Product>? data { get; set; }
        public List<Supplier>? suppliers { get; set; }
        public List<Category>? categories { get; set; }
    }
}

