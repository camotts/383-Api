using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Models.DTOS
{
    public class CartDTO
    {
        public string Url { get; set; }
        public List<Game> Games { get; set; }
    }
}