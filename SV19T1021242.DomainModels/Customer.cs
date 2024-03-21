﻿using System;
namespace SV19T1021242.DomainModels
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = "";
        public string ContactName { get; set; } = "";
        public string Province { get; set; } = ""; 
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public bool IsLocked { get; set; }
    }
}

