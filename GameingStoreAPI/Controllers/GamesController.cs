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

    [Authorize(Roles="Customer, Employee, StoreAdmin")]
    public class GamesController : BaseApiController
    {
        private IGameRepository repo = new GameRepository();

        // GET api/Games
        [AllowAnonymous]
        public HttpResponseMessage GetGames()
        {
            List<GameDTO> ListOfGames = new List<GameDTO>();
            foreach (var item in repo.getListOfGames())
            {

                ListOfGames.Add(TheFactory.Create(item));

            }
            return Request.CreateResponse(HttpStatusCode.OK, ListOfGames);
        }

        // GET api/Games/5
        [AllowAnonymous]
        public HttpResponseMessage GetGame(int id)
        {
            List<GameDTO> gamelist = new List<GameDTO>();
            Game game = repo.getGameById(id);
            if (game == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,"Game not found");
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            GameDTO factoredGame = TheFactory.Create(game);
            gamelist.Add(factoredGame);
            return Request.CreateResponse(HttpStatusCode.OK, gamelist);
            //return game;
        }


        // GET api/Games/?name=""
        [AllowAnonymous]
        public HttpResponseMessage GetGameByName(string name)
        {
            List<GameDTO> gamelist = new List<GameDTO>();
            List<Game> game = repo.getGameByName(name);
            if (game == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Game not found");
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            foreach(Game factoredGame in game)
            {
                gamelist.Add(TheFactory.Create(factoredGame));
            }


            return Request.CreateResponse(HttpStatusCode.OK, gamelist);
            //return game;
        }


        // PUT api/Games/5
        [Authorize(Roles = "StoreAdmin")]
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
            GameDTO factoredGame = TheFactory.Create(game);

            return Request.CreateResponse(HttpStatusCode.OK, factoredGame);
        }


        // POST api/Games
        [Authorize(Roles = "StoreAdmin")]
        public HttpResponseMessage PostGame(Game game)
        {
            if (ModelState.IsValid)
            {

                repo.createGame(game);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, game);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = game.ID }));
                
                GameDTO factoredGame = TheFactory.Create(game);

                return Request.CreateResponse(HttpStatusCode.OK, factoredGame);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        // DELETE api/Games/5
        [Authorize(Roles = "StoreAdmin")]
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