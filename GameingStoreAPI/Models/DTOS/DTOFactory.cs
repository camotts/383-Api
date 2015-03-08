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

    }
}