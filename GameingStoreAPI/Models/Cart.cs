using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public List<Game> Games { get; set; }
        public int CustomerID { get; set; }
    }
}