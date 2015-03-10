using GamingStoreAPI.Models;
using GamingStoreAPI.Models.DTOS;
using GamingStoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamingStoreAPI.Controllers
{
    [Authorize(Roles = "Employee, StoreAdmin")]
    public class TagsController : BaseApiController
    {
         private ITagRepository repo = new TagRepository();

        
        // GET api/Tags
         public HttpResponseMessage GetTags()
         {
             List<TagDTO> ListOfTags = new List<TagDTO>();
             foreach (var item in repo.getListOfTags())
             {

                 ListOfTags.Add(TheFactory.Create(item));

             }
             return Request.CreateResponse(HttpStatusCode.OK, ListOfTags);
         }

        
        // GET api/Tags/5
        public HttpResponseMessage GetTag(int id)
        {
            Tags tag = repo.getTagById(id);
            if (tag == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

           TagDTO factoredTag = TheFactory.Create(tag);

            return Request.CreateResponse(HttpStatusCode.OK, factoredTag);
        }



        [Authorize(Roles = "StoreAdmin")]
        public HttpResponseMessage PostTags(Tags tag)
        {
            if (ModelState.IsValid)
            {

                repo.createTag(tag);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tag);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tag.ID }));
                TagDTO factoredTag = TheFactory.Create(tag);

                return Request.CreateResponse(HttpStatusCode.OK, factoredTag);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        // PUT api/Sales/5
        [Authorize(Roles = "Employee, StoreAdmin")]
        public HttpResponseMessage PutTag(int id, Tags tag)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != tag.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            repo.putTag(id, tag);

            TagDTO factoredTag = TheFactory.Create(tag);

            return Request.CreateResponse(HttpStatusCode.OK, factoredTag);
        }


        [Authorize(Roles = "StoreAdmin")]
        public HttpResponseMessage DeleteTag(int id)
        {
            Tags tag = repo.getTagById(id);
            if (tag == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            repo.deleteTag(tag);

            return Request.CreateResponse(HttpStatusCode.OK, tag);
        }


    }
}
