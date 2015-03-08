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
    public class GamesController : ApiController
    {
        private IGameRepository repo = new GameRepository();

        //[Authorize(Roles="")]
        // GET api/Games
        public IEnumerable<Game> GetGames()
        {
            return repo.getListOfGames();
        }

        [AllowAnonymous]
        // GET api/Games/5
        public Game GetGame(int id)
        {
            Game game = repo.getGameById(id);
            if (game == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return game;
        }

        // PUT api/Games/5
        public HttpResponseMessage PutGame(int id, Game game)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != game.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            repo.putGame(id, game);
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            //}

            return Request.CreateResponse(HttpStatusCode.OK, game);
        }

        // POST api/Games
        public HttpResponseMessage PostGame(Game game)
        {
            if (ModelState.IsValid)
            {

                repo.createGame(game);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, game);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = game.ID }));
                return response;

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Games/5
        public HttpResponseMessage DeleteGame(int id)
        {
            Game game = repo.getGameById(id);
            if (game == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            repo.deleteGame(game);
            

            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            //}
            return Request.CreateResponse(HttpStatusCode.OK, game);
        }


       
       


        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}

    }
}