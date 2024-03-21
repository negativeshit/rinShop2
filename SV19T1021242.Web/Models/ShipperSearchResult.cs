using System;
using SV19T1021242.DomainModels;

namespace SV19T1021242.Web.Models
{
    public class ShipperSearchResult : BasePaginationResult
    {
        public List<Shipper>? data { get; set; }
    }
}

