using GamingStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingStoreAPI.Services
{
    interface ICartRepository
    {
        IEnumerable<Cart> getListOfCarts();
        Cart getCartById(int id);
        void createCart(Cart cart);
        void deleteCart(Cart cart);
        
        void putCart(int id, Cart cart);
        Cart getCartByCustomerId(int id);

    }
}