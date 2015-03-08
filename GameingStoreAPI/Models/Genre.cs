using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Models
{
    public class Genre
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public List<Game> Games { get; set; }

    }
}