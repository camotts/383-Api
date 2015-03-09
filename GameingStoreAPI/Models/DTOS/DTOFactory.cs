using GamingStoreAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace GamingStoreAPI.Models.DTOS
{
    public class DTOFactory
    {

        private UrlHelper urlbuilder { get; set; }

        DataContext db = new DataContext();

        public DTOFactory(HttpRequestMessage request)
        {
            urlbuilder = new UrlHelper(request);
        }

        public UserDTO Create(User user)
        {

            return new UserDTO()
            {
                Url = urlbuilder.Link("GamingStoreRoute", new { id = user.ID }),
                Email = user.Email,
                Role = user.Role
            };

        }

        public GameDTO Create(Game game)
        {

            return new GameDTO()
            {
                Url = urlbuilder.Link("GamingStoreRoute", new { id = game.ID }),
                Name = game.Name,
                ReleaseDate = game.ReleaseDate,
                Price = game.Price,
                InventoryCount = game.InventoryCount,
                Tags = game.Tags,
                Genres = game.Genres
            };
        }

             public SaleDTO Create(Sale sale)
        {

            return new SaleDTO()
            {
                Url = urlbuilder.Link("GamingStoreRoute", new { id = sale.ID }),
                TotalAmount = sale.TotalAmount,
                Date= sale.Date
            };
             }
             public TagDTO Create(Tags tag)
             {
                 return new TagDTO(){

                
                 Url = urlbuilder.Link("GamingStoreRoute", new { id = tag.ID }),
                 Name = tag.Name
          };
    }
             public CartDTO Create(Cart cart)
             {
                 return new CartDTO()
                 {


                     Url = urlbuilder.Link("GamingStoreRoute", new { id = cart.ID }),
                     Games = cart.Games
                   
                 };
             }

             public GenreDTO Create(Genre genre)
             {
                 return new GenreDTO()
                 {


                     Url = urlbuilder.Link("GamingStoreRoute", new { id = genre.ID }),
                     Type =genre.Type
                 };
             }

               
    }
                 
                 
                 
                
        }

    
