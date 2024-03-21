using System;
namespace SV19T1021242.DomainModels
{
	public class Employee
	{
        public int EmployeeId { get; set; }

        public string FullName { get; set; } = "";

        public DateTime BirthDate { get; set; }

        public string Address { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Email { get; set; } = "";

        public string Photo { get; set; } = "";

        public bool IsWorking { get; set; } = true;
    }
}

