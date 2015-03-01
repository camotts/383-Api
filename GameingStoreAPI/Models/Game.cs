using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GamingStoreAPI.Models
{
    public class Game
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal Price { get; set; }

        public int InventoryCount { get; set; }

        public List<Genre> Genres {get; set; }

        public List<Tags> Tags { get; set; }
    }
} 