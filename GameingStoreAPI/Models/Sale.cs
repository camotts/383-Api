using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Models
{
    public class Sale
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public int EmployeeID { get; set; }
    }
}