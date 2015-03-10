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


        public Models.Game getGameByName(string name)
        {
            Game searchGame = db.Games.Where(g => g.Name.Contains(name)).FirstOrDefault();
            return searchGame;
        }


        //public Models.Game getGameByGenre(int id)
        //{
        //    //Game searchGameByGenre = db.Games.Include("db.Genres").Where();

        //}

        //POST 
        public void createGame(Models.Game game)
        {
   
            List<Tags> tags = new List<Tags>();
            List<Genre> genres = new List<Genre>();

            foreach (Tags tag in game.Tags)
            {
                var duplicate = db.Tags.FirstOrDefault(t => t.Name.Equals(tag.Name));
                if (duplicate.Name != null)
                {
                    tags.Add(duplicate);
                }
                else
                {
                    tags.Add(tag);
                }
              
            }

            foreach (Genre genre in game.Genres)
            {
                var duplicate = db.Genres.FirstOrDefault(g => g.Type.Equals(genre.Type));
                if (duplicate.Type != null)
                {
                    genres.Add(duplicate);
                }
                else
                {
                    genres.Add(genre);
                }
              
            }
                game.Tags = tags;
                game.Genres = genres;
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
            modifyGame.Price = game.Price;
            modifyGame.InventoryCount = game.InventoryCount;
            List<Tags> tags = new List<Tags>();
            List<Genre> genres = new List<Genre>();

            foreach (Tags tag in game.Tags)
            {
                var duplicate = db.Tags.FirstOrDefault(t => t.Name.Equals(tag.Name));
                if (duplicate == null)
                {
                    modifyGame.Tags = game.Tags;
                }
                
            }

            foreach (Genre genre in game.Genres)
            {
                var duplicate = db.Genres.FirstOrDefault(g => g.Type.Equals(genre.Type));
                if (duplicate == null)
                {
                    modifyGame.Genres = game.Genres;
                }
                
            }

            modifyGame.ReleaseDate = game.ReleaseDate;
            db.Entry(modifyGame).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
    