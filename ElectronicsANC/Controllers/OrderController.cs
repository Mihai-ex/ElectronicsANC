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

        // GET: Order
        public ActionResult Index()
        {
            List<OrderModel> orders = _orderRepository.GetAllOrders();

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
            return View("CreateOrder");
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                OrderModel orderModel = new OrderModel();
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
    }
}
