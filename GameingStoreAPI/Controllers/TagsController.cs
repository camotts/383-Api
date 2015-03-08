using GamingStoreAPI.Models;
using GamingStoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamingStoreAPI.Controllers
{
    public class TagsController : BaseApiController
    {
         private ITagRepository repo = new TagRepository();

        //[Authorize(Roles="")]
        // GET api/Tags
         public IEnumerable<Tags> GetTags()
         {
             return repo.getListOfTags();
         }

        [AllowAnonymous]
        // GET api/Tags/5
        public Tags GetTag(int id)
        {
            Tags tag = repo.getTagById(id);
            if (tag == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tag;
        }
    }
}
