using GamingStoreAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GamingStoreAPI.Services
{
    interface IUserRepository
    {

        IEnumerable<User> getListOfUsers();
        User getUserById(int id);
        void createUser(User user);
        void deleteUser(User user);
        //string getApiKey();
        void putUser(int id, User user);


        
        




    }
}
