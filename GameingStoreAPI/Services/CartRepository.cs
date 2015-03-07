using GamingStoreAPI.DAL;
using GamingStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Services
{
    public class CartRepository:ICartRepository
    {



        private DataContext db = new DataContext();

        public IEnumerable<Cart> getListOfCarts()
        {
            return db.Carts.AsEnumerable();
        }


        public Models.Cart getCartById(int Id)
        {
            Cart searchCart = db.Carts.Find(Id);
            return searchCart;
        }

        public Models.Cart getCartByCustomerId(int Id)
        {
            Cart searchCustomer = db.Carts.Find(Id);
            return searchCustomer;
        }

        //POST 
        public void createCart(Models.Cart cart){
        

            db.Carts.Add(cart);
            db.SaveChanges();
        }



        public void deleteCart(Models.Cart cart)
        {
            db.Carts.Remove(cart);
            db.SaveChanges();

        }



        public void putCart(int id, Models.Cart cart)
        {
            Cart modifyCart = getCartById(id);

            modifyCart.Games = cart.Games;
            
            db.Entry(modifyCart).State = EntityState.Modified;
        }


    }
}