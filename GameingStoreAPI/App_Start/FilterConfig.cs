using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using GamingStoreAPI.Authentication;

namespace GamingStoreAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //GlobalConfiguration.Configuration.MessageHandlers.Add(new AuthenticationHandler());
        }
    }
}