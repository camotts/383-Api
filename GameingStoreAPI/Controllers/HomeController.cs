using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GamingStoreAPI.Models;
using GamingStoreAPI.DAL;
using System.Web.UI.WebControls;

namespace GamingStoreAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult LoginPage()
        {
            //This part below might serve to authenticate with
            //password when logging in, need javascript 1st though
            /*
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("LoginPage", "Home");
            */
            return View(); //remove if code above is worked out
        }

        public ActionResult Index()
        {
            return View();
        }

        private DataContext db = new DataContext();

    }
}
