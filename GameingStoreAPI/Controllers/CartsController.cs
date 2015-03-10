using GamingStoreAPI.Models;
using GamingStoreAPI.Models.DTOS;
using GamingStoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamingStoreAPI.Controllers
{
    [Authorize(Roles = "Customer, Employee, StoreAdmin")]
    public class CartsController : BaseApiController
    {
        private ICartRepository repo = new CartRepository();

        
        // GET api/Games
        [Authorize(Roles = "Employee, StoreAdmin")]
        public HttpResponseMessage GetListOfCarts()
        {
            List<CartDTO> ListOfCarts = new List<CartDTO>();
            foreach (var item in repo.getListOfCarts())
            {

                ListOfCarts.Add(TheFactory.Create(item));

            }
            return Request.CreateResponse(HttpStatusCode.OK, ListOfCarts);
        }

        // GET api/Games/5
        [Authorize(Roles = "Employee, StoreAdmin")]
        public HttpResponseMessage GetCart(int id)
        {
            Cart cart = repo.getCartById(id);
            if (cart == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            CartDTO factoredCart = TheFactory.Create(cart);

            return Request.CreateResponse(HttpStatusCode.OK, factoredCart);
        }

        // PUT api/Games/5
        [Authorize(Roles = "StoreAdmin")]
        public HttpResponseMessage PutCart(int id, Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != cart.ID)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            repo.putCart(id, cart);
            CartDTO factoredCart = TheFactory.Create(cart);

            return Request.CreateResponse(HttpStatusCode.OK, factoredCart);
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            //}

            
        }


        // POST api/Games
        [Authorize(Roles="Customer, StoreAdmin")]
        public HttpResponseMessage PostCart(Cart cart)
        {
            if (ModelState.IsValid)
            {
                repo.createCart(cart);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, cart);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = cart.ID }));
                CartDTO factoredCart = TheFactory.Create(cart);

                return Request.CreateResponse(HttpStatusCode.OK, factoredCart);

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }


        // DELETE api/Games/5
        [Authorize(Roles = "StoreAdmin")]
        public HttpResponseMessage DeleteCart(int id)
        {
             Cart cart = repo.getCartById(id);
            if (cart == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            repo.deleteCart(cart);


            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            //}
            return Request.CreateResponse(HttpStatusCode.OK, cart);
        }



        public HttpResponseMessage GetCustomer(int id)
        {
            Cart cart = repo.getCartById(id);

            if (cart == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, cart);
        }







        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}

    }
}
  