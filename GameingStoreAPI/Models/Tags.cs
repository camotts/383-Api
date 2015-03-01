using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Models
{
    public class Tags
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<Game> Games { get; set; }
    }
}