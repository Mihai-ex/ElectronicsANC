using ElectronicsANC.Models;
using ElectronicsANC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElectronicsANC.Controllers
{
    public class OrderController : Controller
    {
        private OrderRepository _orderRepository = new OrderRepository();
        private ProductRepository _productRepository = new ProductRepository();

        // GET: Order
        public ActionResult Index(string sortOrder)
        {
            List<OrderModel> orders = _orderRepository.GetAllOrders();

            ViewBag.DateSortParam = string.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.QuantitySortParam = sortOrder == "quantity" ? "quantity_desc" : "quantity";
            ViewBag.PriceSortParam = sortOrder == "price" ? "price_desc" : "price";

            orders = SortProducts(orders, sortOrder);

            return View("Index", orders);
        }

        // GET: Order/Details/5
        public ActionResult Details(Guid id)
        {
            OrderModel orderModel = _orderRepository.GetOrderById(id);

            return View("DetailsOrder", orderModel);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            var items = _productRepository.GetAllProducts();

            if (items != null)
            {
                ViewBag.shoppingProducts = items;
            }

            return View("CreateOrder");
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                OrderModel orderModel = new OrderModel();
                orderModel.IdProduct = Guid.Parse(Request.Form["OrderProducts"]);
                UpdateModel(orderModel);

                _orderRepository.InsertOrder(orderModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateOrder");
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(Guid id)
        {
            OrderModel orderModel = _orderRepository.GetOrderById(id);

            return View("EditOrder", orderModel);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                OrderModel orderModel = new OrderModel();
                UpdateModel(orderModel);

                _orderRepository.UpdateOrder(orderModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditORder");
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(Guid id)
        {
            OrderModel orderModel = _orderRepository.GetOrderById(id);

            return View("DeleteOrder");
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                _orderRepository.DeleteOrder(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteOrder");
            }
        }

        private List<OrderModel> SortProducts(List<OrderModel> temp, string sortOrder)
        {
            switch (sortOrder)
            {
                case "date_desc":
                    return _orderRepository.OrderByDescendingParameter(temp, "Date");
                case "price_desc":
                    return _orderRepository.OrderByDescendingParameter(temp, "Price");
                case "quantity_desc":
                    return _orderRepository.OrderByDescendingParameter(temp, "Quantity");
                case "price":
                    return _orderRepository.OrderByAscendingParameter(temp, "Price");
                case "warranty":
                    return _orderRepository.OrderByAscendingParameter(temp, "Quantity");
                default:
                    return _orderRepository.OrderByAscendingParameter(temp, "Date");
            }
        }
    }
}
