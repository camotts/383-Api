using GamingStoreAPI.DAL;
using GamingStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Services
{
    public class GameRepository:IGameRepository
    {

    
        
        private DataContext db = new DataContext();

        public IEnumerable<Game> getListOfGames()
        {
            return db.Games.AsEnumerable();
        }


        public Models.Game getGameById(int Id)
        {
            Game searchGame = db.Games.Find(Id);
            return searchGame;
        }


        public List<Models.Game> getGameByName(string name)
        {
            List<Game> searchGame = db.Games.Where(g => g.Name.Contains(name)).ToList();
            return searchGame;
        }


        //public Models.Game getGameByGenre(int id)
        //{
        //    //Game searchGameByGenre = db.Games.Include("db.Genres").Where();

        //}

        //POST 
        public void createGame(Models.Game game)
        {          
                db.Games.Add(game);
                db.SaveChanges();   
        }

    

        public void deleteGame(Models.Game game)
        {
            db.Games.Remove(game); 
            db.SaveChanges();
            
        }

       

        public void putGame(int id, Models.Game game)
        {
            Game modifyGame = getGameById(id);
            modifyGame.Name = game.Name;
            modifyGame.Genres = game.Genres;
            modifyGame.Price = game.Price;
            modifyGame.InventoryCount = game.InventoryCount;
            modifyGame.Tags = game.Tags;
            modifyGame.ReleaseDate = game.ReleaseDate;
            db.Entry(modifyGame).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
    