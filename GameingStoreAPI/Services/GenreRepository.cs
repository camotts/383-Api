using GamingStoreAPI.DAL;
using GamingStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Services
{
    public class GenreRepository:IGenreRepository
    {

        private DataContext db = new DataContext();

        public IEnumerable<Genre> getListOfGenres()
        {
            return db.Genres.AsEnumerable();
        }


        public Models.Genre getGenreById(int Id)
        {
            Genre searchGenre = db.Genres.Find(Id);
            return searchGenre;
        }

      
        //POST 
        public void createGenre(Models.Genre genre)
        {
            db.Genres.Add(genre);
            db.SaveChanges();
        }



        public void deleteGenre(Models.Genre genre)
        {
            db.Genres.Remove(genre);
            db.SaveChanges();

        }



        public void putGenre(int id, Models.Genre genre)
        {
            Genre modifyGenre = getGenreById(id);
            modifyGenre.Type = genre.Type;
           
            db.Entry(modifyGenre).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}