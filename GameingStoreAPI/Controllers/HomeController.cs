using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GamingStoreAPI.Models;
using GamingStoreAPI.DAL;

namespace GamingStoreAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private DataContext db = new DataContext();

    }
}
