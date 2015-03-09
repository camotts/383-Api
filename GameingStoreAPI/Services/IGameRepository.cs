using GamingStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Services
{
    interface IGameRepository
    {

        IEnumerable<Game> getListOfGames();
        Game getGameById(int id);
        Game getGameByName(string name);
        void createGame(Game game);
        void deleteGame(Game game);
        //string getApiKey();
        void putGame(int id, Game game);
    }
}