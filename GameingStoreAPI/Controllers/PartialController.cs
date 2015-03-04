using GamingStoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GamingStoreAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GamingStoreAPI.Controllers
{
    public class PartialController : Controller
    {
        private IUserRepository repo = new UserRepository();

        //
        // GET: /Partial/

        public ActionResult UserIndex(Object item)
        {
           
            return View();
        }

        //
        // GET: /Partial/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Partial/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Partial/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Partial/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Partial/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Partial/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Partial/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
