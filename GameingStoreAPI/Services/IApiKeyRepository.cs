using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingStoreAPI.Services
{
    interface IApiKeyRepository
    {

        Models.User getApiKey(string email, string password);

    }
}
