using GamingStoreAPI.Models.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GamingStoreAPI.Controllers
{
    public class BaseApiController : ApiController
    {

        DTOFactory _factory;

        protected DTOFactory TheFactory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = new DTOFactory(this.Request);
                }
                return _factory;
            }
        }

    }
}
