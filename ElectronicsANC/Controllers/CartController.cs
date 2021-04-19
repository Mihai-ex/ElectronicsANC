using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class CartController : Controller
    {
        private CartRepository _cartRepository = new CartRepository();

        // GET: Cart
        public ActionResult Index()
        {
            List<CartModel> cartModels = _cartRepository.GetAllCarts();

            return View("Index", cartModels);
        }

        // GET: Cart/Details/5
        public ActionResult Details(Guid id)
        {
            CartModel cartModel = _cartRepository.GetCartById(id);

            return View("DetailsCart", cartModel);
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View("CreateCart");
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                CartModel cartModel = new CartModel();
                UpdateModel(cartModel);

                _cartRepository.InsertCart(cartModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateCart");
            }
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(Guid id)
        {
            CartModel cartModel = _cartRepository.GetCartById(id);

            return View("EditCart", cartModel);
        }

        // POST: Cart/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                CartModel cartModel = new CartModel();
                UpdateModel(cartModel);

                _cartRepository.UpdateCart(cartModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditCart");
            }
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(Guid id)
        {
            CartModel cartModel = _cartRepository.GetCartById(id);

            return View("DeleteCart", cartModel);
        }

        // POST: Cart/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                _cartRepository.DeleteCart(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteCart");
            }
        }
    }
}
