using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Models.DTOS
{
    public class SaleDTO
    {
        public string Url { get; set; }
        public DateTime Date { get; set; }


        public decimal TotalAmount { get; set; }

    }
}