using ElectronicsANC.Models;
using ElectronicsANC.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Repository
{
    public class ShoppingCartRepository
    {
        public ElectronicsDbDataContext dbContext;

        public ShoppingCartRepository()
        {
            dbContext = new ElectronicsDbDataContext();
        }

        public ShoppingCartRepository(ElectronicsDbDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ShoppingCartModel> GetAllShoppingCarts()
        {
            List<ShoppingCartModel> shoppingCartList = new List<ShoppingCartModel>();

            foreach (ShoppingCart dbShoppingCart in dbContext.ShoppingCarts)
                AddDbObjectToModelCollection(shoppingCartList, dbShoppingCart);

            return shoppingCartList;
        }

        public ShoppingCartModel GetShoppingCartById(Guid id)
        {
            var shoppingCart = dbContext.ShoppingCarts.FirstOrDefault(x => x.IdShoppingCart == id);

            return MapDbObjectToModel(shoppingCart);
        }

        public List<ShoppingCartModel> GetShoppingCartsByPrice(decimal price)
        {
            List<ShoppingCartModel> shoppingCartList = new List<ShoppingCartModel>();

            foreach (ShoppingCart dbShoppingCart in dbContext.ShoppingCarts)
                if (dbShoppingCart.Price.Equals(price))
                    AddDbObjectToModelCollection(shoppingCartList, dbShoppingCart);

            return shoppingCartList;
        }

        public List<ShoppingCartModel> GetShoppingCartsByQuantity(int amount)
        {
            List<ShoppingCartModel> shoppingCartList = new List<ShoppingCartModel>();

            foreach (ShoppingCart dbShoppingCart in dbContext.ShoppingCarts)
                if (dbShoppingCart.Quantity.Equals(amount))
                    AddDbObjectToModelCollection(shoppingCartList, dbShoppingCart);

            return shoppingCartList;
        }

        public void InsertShoppingCart(ShoppingCartModel shoppingCart)
        {
            shoppingCart.IdShoppingCart = Guid.NewGuid();

            dbContext.ShoppingCarts.InsertOnSubmit(MapModelToDbObject(shoppingCart));
            dbContext.SubmitChanges();
        }

        public void UpdateShoppingCart(ShoppingCartModel shoppingCart)
        {
            ShoppingCart dbShoppingCart = dbContext.ShoppingCarts.FirstOrDefault(x => x.IdShoppingCart == shoppingCart.IdShoppingCart);

            if(dbShoppingCart != null)
            {
                dbShoppingCart.IdShoppingCart = shoppingCart.IdShoppingCart;
                dbShoppingCart.IdProduct = shoppingCart.IdProduct;
                dbShoppingCart.Price = shoppingCart.Price;
                dbShoppingCart.Quantity = shoppingCart.Quantity;
                dbContext.SubmitChanges();
            }
        }

        public void DeleteShoppingCart(Guid id)
        {
            ShoppingCart dbShoppingCart = dbContext.ShoppingCarts.FirstOrDefault(x => x.IdShoppingCart == id);

            if(dbShoppingCart != null)
            {
                dbContext.ShoppingCarts.DeleteOnSubmit(dbShoppingCart);
                dbContext.SubmitChanges();
            }
        }

        private void AddDbObjectToModelCollection(List<ShoppingCartModel> shoppingCartList, ShoppingCart dbShoppingCart)
        {
            shoppingCartList.Add(MapDbObjectToModel(dbShoppingCart));
        }

        private ShoppingCartModel MapDbObjectToModel(ShoppingCart dbShoppingCart)
        {
            ShoppingCartModel shoppingCart = new ShoppingCartModel();

            if(dbShoppingCart != null)
            {
                shoppingCart.IdShoppingCart = dbShoppingCart.IdShoppingCart;
                shoppingCart.IdProduct = dbShoppingCart.IdProduct;
                shoppingCart.Price = dbShoppingCart.Price;
                shoppingCart.Quantity = dbShoppingCart.Quantity;

                return shoppingCart;
            }

            return null;
        }

        private ShoppingCart MapModelToDbObject(ShoppingCartModel shoppingCart)
        {
            ShoppingCart dbShoppingCart = new ShoppingCart();

            if(shoppingCart != null)
            {
                dbShoppingCart.IdShoppingCart = shoppingCart.IdShoppingCart;
                dbShoppingCart.IdProduct = shoppingCart.IdProduct;
                dbShoppingCart.Quantity = shoppingCart.Quantity;
                var finalPrice = shoppingCart.Quantity * shoppingCart.Price;
                dbShoppingCart.Price = finalPrice;

                return dbShoppingCart;
            }

            return null;
        }
    }
}