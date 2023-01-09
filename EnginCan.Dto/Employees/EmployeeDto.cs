using System;
using System.Collections.Generic;
using System.Text;

namespace EnginCan.Dto.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Type { get; set; }
        public string EmployeeCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime JobStartDate { get; set; }
        public int? UserId { get; set; }

    }
}
