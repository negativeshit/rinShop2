using System;
using System.Collections.Generic;
using SV19T1021242.DomainModels;

namespace SV19T1021242.Web.Models
{
    public class OrderDetailModel
    {
        public Order Order { get; set; }
        public List<OrderDetail> Details { get; set; }
    }
}

