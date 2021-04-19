using ElectronicsANC.Models;
using ElectronicsANC.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Repository
{
    public class CartRepository
    {
        public ElectronicsDbDataContext dbContext;

        public CartRepository()
        {
            dbContext = new ElectronicsDbDataContext();
        }

        public CartRepository(ElectronicsDbDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<CartModel> GetAllCarts()
        {
            List<CartModel> cartList = new List<CartModel>();

            foreach(Cart dbCart in dbContext.Carts)
                AddDbObjectToModelCollection(cartList, dbCart);

            return cartList;
        }

        public CartModel GetCartById(Guid id)
        {
            var cart = dbContext.Carts.FirstOrDefault(x => x.IdCart == id);

            return MapDbObjectToModel(cart);
        }

        public List<CartModel> GetCartsByDate(DateTime cartDate)
        {
            List<CartModel> cartList = new List<CartModel>();

            foreach (Cart dbCart in dbContext.Carts)
                if (dbCart.Date == cartDate)
                    AddDbObjectToModelCollection(cartList, dbCart);

            return cartList;
        }

        public void InsertCart(CartModel cart)
        {
            cart.IdCart = Guid.NewGuid();

            dbContext.Carts.InsertOnSubmit(MapModelToDbObject(cart));
            dbContext.SubmitChanges();
        }

        public void UpdateCart(CartModel cart)
        {
            Cart dbCart = dbContext.Carts.FirstOrDefault(x => x.IdCart == cart.IdCart);

            if(dbCart != null)
            {
                dbCart.IdCart = cart.IdCart;
                dbCart.IdMember = cart.IdMember;
                dbCart.IdShoppingCart = cart.IdShoppingCart;
                dbCart.Date = cart.Date;
                dbContext.SubmitChanges();
            }
        }

        public void DeleteCart(Guid id)
        {
            Cart dbCart = dbContext.Carts.FirstOrDefault(x => x.IdCart == id);

            if(dbCart != null)
            {
                dbContext.Carts.DeleteOnSubmit(dbCart);
                dbContext.SubmitChanges();
            }
        }

        private void AddDbObjectToModelCollection(List<CartModel> cartList, Cart dbCart)
        {
            cartList.Add(MapDbObjectToModel(dbCart));
        }

        private CartModel MapDbObjectToModel(Cart dbCart)
        {
            CartModel cart = new CartModel();

            if(dbCart != null)
            {
                cart.IdCart = dbCart.IdCart;
                cart.IdMember = dbCart.IdMember;
                cart.IdShoppingCart = dbCart.IdShoppingCart;
                cart.Date = dbCart.Date;

                return cart;
            }

            return null;
        }

        private Cart MapModelToDbObject(CartModel cart)
        {
            Cart dbCart = new Cart();

            if(cart != null)
            {
                dbCart.IdCart = cart.IdCart;
                dbCart.IdMember = cart.IdMember;
                dbCart.IdShoppingCart = cart.IdShoppingCart;
                dbCart.Date = cart.Date;

                return dbCart;
            }

            return null;
        }
    }
}