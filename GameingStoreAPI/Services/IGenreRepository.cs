using GamingStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Services
{
    interface IGenreRepository
    {
        IEnumerable<Genre> getListOfGenres();
        Genre getGenreById(int id);
        void createGenre(Genre genre);
        void deleteGenre(Genre genre);
      
        void putGenre(int id, Genre genre);
    }
}