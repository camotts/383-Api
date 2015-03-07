using GamingStoreAPI.Models;
using GamingStoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GamingStoreAPI.Controllers
{
    public class CartsController : ApiController
    {
        private ICartRepository repo = new CartRepository();

        //[Authorize(Roles="")]
        // GET api/Games
        public IEnumerable<Cart> GetCarts()
        {
            return repo.getListOfCarts();
        }

        [AllowAnonymous]
        // GET api/Games/5
        public Cart GetCart(int id)
        {
            Cart cart = repo.getCartById(id);
            if (cart == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return cart;
        }

        // PUT api/Games/5
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
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            //}

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Games
        public HttpResponseMessage PostGame(Cart cart)
        {
            if (ModelState.IsValid)
            {
                repo.createCart(cart);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, cart);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = cart.ID }));
                return response;

            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Games/5
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
        public Cart GetCustomer(int id)
        {
            Cart cart = repo.getCartByCustomerId(id);

            if (cart == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return cart;
        }







        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}

    }
}
  