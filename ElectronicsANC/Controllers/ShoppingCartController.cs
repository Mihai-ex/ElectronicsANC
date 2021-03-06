using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ShoppingCartRepository _shoppingCartRepository = new ShoppingCartRepository();
        private ProductRepository _productRepository = new ProductRepository();

        // GET: ShoppingCart
        public ActionResult Index(string sortOrder)
        {
            List<ShoppingCartModel> shoppingCarts = _shoppingCartRepository.GetAllShoppingCarts();

            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "quantity_desc" : "";
            ViewBag.PriceSortParam = sortOrder == "price" ? "price_desc" : "price";

            shoppingCarts = SortCarts(shoppingCarts, sortOrder);

            foreach(var cart in shoppingCarts)
            {
                var product = _productRepository.GetProdctById(cart.IdProduct);
                cart.ProductName = product.ProductName;
            }

            return View("Index", shoppingCarts);
        }

        // GET: ShoppingCart/Details/5
        public ActionResult Details(Guid id)
        {
            ShoppingCartModel shoppingCartModel = _shoppingCartRepository.GetShoppingCartById(id);

            return View("DetailsShoppingCart", shoppingCartModel);
        }

        // GET: ShoppingCart/Create
        public ActionResult Create()
        {
            var items = _productRepository.GetAllProducts();

            if (items != null)
            {
                ViewBag.shoppingProducts = items;
            }

            return View("CreateShoppingCart");
        }

        // POST: ShoppingCart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ShoppingCartModel shoppingCartModel = new ShoppingCartModel();                
                UpdateModel(shoppingCartModel);

                if (User.Identity.IsAuthenticated)
                    if (User.IsInRole("Admin"))
                        if (shoppingCartModel.IdProduct == null)
                            shoppingCartModel.IdProduct = Guid.Parse(Request.Form["ShoppingProducts"]);

                if (shoppingCartModel.Quantity == 0)
                    return View("CreateShoppingCart");

                UpdateModel(shoppingCartModel);

                _shoppingCartRepository.InsertShoppingCart(shoppingCartModel);
                
                if(User.Identity.IsAuthenticated)
                    if(User.IsInRole("Admin"))
                        return RedirectToAction("Index");

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View("CreateShoppingCart");
            }
        }

        // GET: ShoppingCart/Edit/5
        public ActionResult Edit(Guid id)
        {
            ShoppingCartModel shoppingCartModel = _shoppingCartRepository.GetShoppingCartById(id);

            return View("EditShoppingCart", shoppingCartModel);
        }

        // POST: ShoppingCart/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                ShoppingCartModel shoppingCartModel = _shoppingCartRepository.GetShoppingCartById(id);
                UpdateModel(shoppingCartModel);

                _shoppingCartRepository.UpdateShoppingCart(shoppingCartModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditShoppingCart");
            }
        }

        // GET: ShoppingCart/Delete/5
        public ActionResult Delete(Guid id)
        {
            ShoppingCartModel shoppingCartModel = _shoppingCartRepository.GetShoppingCartById(id);

            return View("DeleteShoppingCart", shoppingCartModel);
        }

        // POST: ShoppingCart/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                _shoppingCartRepository.DeleteShoppingCart(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteShoppingCart");
            }
        }

        private List<ShoppingCartModel> SortCarts(List<ShoppingCartModel> temp, string sortOrder)
        {
            switch (sortOrder)
            {
                case "quantity_desc":
                    return _shoppingCartRepository.OrderByDescendingParameter(temp, "Quantity");
                case "price_desc":
                    return _shoppingCartRepository.OrderByDescendingParameter(temp, "Price");
                case "price":
                    return _shoppingCartRepository.OrderByAscendingParameter(temp, "Price");
                default:
                    return _shoppingCartRepository.OrderByAscendingParameter(temp, "Quantity");
            }
        }
    }
}
