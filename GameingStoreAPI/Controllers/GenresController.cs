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
    [Authorize(Roles = "Customer, Employee, StoreAdmin")]
    public class GenresController : BaseApiController
    {
         
        private IGenreRepository repo = new GenreRepository();

        
        // GET api/Games
        public HttpResponseMessage GetGenres()
        {
            List<GenreDTO> ListOfGenres = new List<GenreDTO>();
            foreach (var item in repo.getListOfGenres())
            {

                ListOfGenres.Add(TheFactory.Create(item));

            }
            return Request.CreateResponse(HttpStatusCode.OK, ListOfGenres);
        }

        
        // GET api/Games/5
        public HttpResponseMessage GetGenre(int id)
        {
            Genre genre = repo.getGenreById(id);
            if (genre == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Game not found");
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            GenreDTO factoredGenre = TheFactory.Create(genre);

            return Request.CreateResponse(HttpStatusCode.OK, factoredGenre);
            //return game;
        }

        // PUT api/Games/5
        [Authorize(Roles = "StoreAdmin")]
        public HttpResponseMessage PutGenre(int id, Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != genre.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            repo.putGenre(id, genre);
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            //}
            GenreDTO factoredGenre = TheFactory.Create(genre);

            return Request.CreateResponse(HttpStatusCode.OK, factoredGenre);
        }

        // POST api/Games
        [Authorize(Roles = "StoreAdmin")]
        public HttpResponseMessage PostGenre(Genre genre)
        {
            if (ModelState.IsValid)
            {
                repo.createGenre(genre);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, genre);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = genre.ID }));
                
                GenreDTO factoredGenre = TheFactory.Create(genre);

                return Request.CreateResponse(HttpStatusCode.OK, factoredGenre);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Games/5
        [Authorize(Roles = "StoreAdmin")]
        public HttpResponseMessage DeleteGenre(int id)
        {
            Genre genre = repo.getGenreById(id);
            if (genre == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            repo.deleteGenre(genre);
            

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            //}
            return Request.CreateResponse(HttpStatusCode.OK, genre);
        }


       
       


        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}

    }
}
