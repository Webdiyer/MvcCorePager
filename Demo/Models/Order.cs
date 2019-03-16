using System;
using System.Collections.Generic;

namespace Demo.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string EmployeeName { get; set; }
    }
}
