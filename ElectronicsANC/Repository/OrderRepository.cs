using ElectronicsANC.Models;
using ElectronicsANC.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicsANC.Repository
{
    public class OrderRepository
    {
        public ElectronicsDbDataContext dbContext;

        public OrderRepository()
        {
            dbContext = new ElectronicsDbDataContext();
        }

        public OrderRepository(ElectronicsDbDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<OrderModel> GetAllOrders()
        {
            List<OrderModel> orderList = new List<OrderModel>();

            foreach(Order dbOrder in dbContext.Orders)
                AddDbObjectToModelCollection(orderList, dbOrder);

            return orderList;
        }

        public OrderModel GetOrderById(Guid id)
        {
            var order = dbContext.Orders.FirstOrDefault(x => x.IdOrder == id);

            return MapDbObjectToModel(order);
        }

        public List<OrderModel> GetOrdersByDate(DateTime dateTime)
        {
            List<OrderModel> orderList = new List<OrderModel>();

            foreach (Order dbOrder in dbContext.Orders)
                if (dbOrder.DateOrder == dateTime)
                    AddDbObjectToModelCollection(orderList, dbOrder);

            return orderList;
        }

        public List<OrderModel> GetOrdersByQuantity(int amount)
        {
            List<OrderModel> orderList = new List<OrderModel>();

            foreach (Order dbOrder in dbContext.Orders)
                if (dbOrder.Quantity == amount)
                    AddDbObjectToModelCollection(orderList, dbOrder);

            return orderList;
        }

        public List<OrderModel> GetOrdersByPrice(decimal price)
        {
            List<OrderModel> orderList = new List<OrderModel>();

            foreach (Order dbOrder in dbContext.Orders)
                if (dbOrder.Price == price)
                    AddDbObjectToModelCollection(orderList, dbOrder);

            return orderList;
        }

        public List<OrderModel> OrderByDescendingParameter(List<OrderModel> models, string parameter)
        {
            if (parameter == "Date")
                return models.OrderByDescending(x => x.DateOrder).ToList();

            if (parameter == "Price")
                return models.OrderByDescending(x => x.Price).ToList();

            if (parameter == "Quantity")
                return models.OrderByDescending(x => x.Quantity).ToList();

            return models;
        }
        public List<OrderModel> OrderByAscendingParameter(List<OrderModel> models, string parameter)
        {
            if (parameter == "Date")
                return models.OrderBy(x => x.DateOrder).ToList();

            if (parameter == "Price")
                return models.OrderBy(x => x.Price).ToList();

            if (parameter == "Quantity")
                return models.OrderBy(x => x.Quantity).ToList();

            return models;
        }

        public void InsertOrder(OrderModel order)
        {
            order.IdOrder = Guid.NewGuid();

            dbContext.Orders.InsertOnSubmit(MapModelToDbObject(order));
            dbContext.SubmitChanges();
        }

        public void UpdateOrder(OrderModel order)
        {
            Order dbOrder = dbContext.Orders.FirstOrDefault(x => x.IdOrder == order.IdOrder);

            if(dbOrder != null)
            {
                dbOrder.IdOrder = order.IdOrder;
                dbOrder.IdProduct = order.IdProduct;
                dbOrder.DateOrder = order.DateOrder;
                dbOrder.Price = order.Price;
                dbOrder.Quantity = order.Quantity;
                dbContext.SubmitChanges();
            }
        }

        public void DeleteOrder(Guid id)
        {
            Order dbOrder = dbContext.Orders.FirstOrDefault(x => x.IdOrder == id);

            if(dbOrder != null)
            {
                dbContext.Orders.DeleteOnSubmit(dbOrder);
                dbContext.SubmitChanges();
            }
        }

        private void AddDbObjectToModelCollection(List<OrderModel> orderList, Order dbOrder)
        {
            orderList.Add(MapDbObjectToModel(dbOrder));
        }

        private OrderModel MapDbObjectToModel(Order dbOrder)
        {
            OrderModel order = new OrderModel();

            if(dbOrder != null)
            {
                order.IdOrder = dbOrder.IdOrder;
                order.IdProduct = dbOrder.IdProduct;
                order.DateOrder = dbOrder.DateOrder;
                order.Quantity = dbOrder.Quantity;
                order.Price = dbOrder.Price;

                return order;
            }

            return null;
        }

        private Order MapModelToDbObject(OrderModel order)
        {
            Order dbOrder = new Order();

            if (order != null)
            {
                dbOrder.IdOrder = order.IdOrder;
                dbOrder.IdProduct = order.IdProduct;
                dbOrder.DateOrder = order.DateOrder;
                dbOrder.Quantity = order.Quantity;
                var finalPrice = order.Price * order.Quantity;
                dbOrder.Price = finalPrice;

                return dbOrder;
            }

            return null;
        }
    }
}